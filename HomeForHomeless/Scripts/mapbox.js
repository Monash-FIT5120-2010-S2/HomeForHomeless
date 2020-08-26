var options = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0
};

function success(pos) {
    crd = pos.coords;
    loadMap(crd.longitude, crd.latitude);
};

function error(err) {
    console.log(`ERROR(${err.code}): ${err.message}`);
    loadMap(-73.935242, 40.730610);
};

navigator.geolocation.getCurrentPosition(success, error, options);

function loadMap(lng, lat) {

    var locations = [];
    // The first step is obtain all the latitude and longitude from the HTML
    // The below is a simple jQuery selector
    $(".coordinates").each(function () {
        var Longitude = $(".Longitude", this).text();
        var Latitude = $(".Latitude", this).text();
        var Address = $(".Address", this).text();
        // Create a point data structure to hold the values.
        var point = {
            "Latitude": Latitude,
            "Longitude": Longitude,
            "Address": Address
        };
        // Push them all into an array.
        locations.push(point);
    });

    var data = [];
    for (i = 0; i < locations.length; i++) {
        var feature = {
            "type": "Feature",
            "properties": {
                "Address": locations[i].Address,
                "icon": "town-hall-15",
            },
            "geometry": {
                "type": "Point",
                "coordinates": [locations[i].Longitude, locations[i].Latitude]
            }
        };
        data.push(feature)
    }

    function forwardGeocoder(query) {
        var matchingFeatures = [];
        for (var i = 0; i < data.length; i++) {
            var feature = data[i];
            // handle queries with different capitalization than the source data by calling toLowerCase()
            if (
                feature.properties.Address
                    .toLowerCase()
                    .search(query.toLowerCase()) !== -1
            ) {
                feature['place_name'] = '🌲' + feature.properties.Address;
                feature['center'] = feature.geometry.coordinates;
                feature['place_type'] = ['Recycling Centre'];
                matchingFeatures.push(feature);
            }
        }
        return matchingFeatures;
    }

    mapboxgl.accessToken = 'pk.eyJ1IjoiYW1iZXIyIiwiYSI6ImNrZTlzcTYyMjA3MHcydG1qeTYwZzEwYTkifQ.raV92EHGPvS1cDfhgwPWrg';
    var map = new mapboxgl.Map({
        container: 'map', // container id
        style: 'mapbox://styles/mapbox/streets-v9', // stylesheet location
        center: [lng, lat], // starting position [lng, lat]
        zoom: 9 // starting zoom
    });

    var canvas = map.getCanvasContainer();

    var start = [lng, lat];

    function getRoute(end) {
        // make a directions request using cycling profile
        // an arbitrary start will always be the same
        // only the end or destination will change

        var url = 'https://api.mapbox.com/directions/v5/mapbox/walking/' + start[0] + ',' + start[1] + ';' + end[0] + ',' + end[1] + '?steps=true&geometries=geojson&access_token=' + mapboxgl.accessToken;

        // make an XHR request https://developer.mozilla.org/en-US/docs/Web/API/XMLHttpRequest
        var req = new XMLHttpRequest();
        req.open('GET', url, true);
        req.onload = function () {
            var json = JSON.parse(req.response);
            var data = json.routes[0];
            var route = data.geometry.coordinates;
            var geojson = {
                type: 'Feature',
                properties: {},
                geometry: {
                    type: 'LineString',
                    coordinates: route
                }
            };
            // if the route already exists on the map, reset it using setData
            if (map.getSource('route')) {
                map.getSource('route').setData(geojson);
            } else { // otherwise, make a new request
                map.addLayer({
                    id: 'route',
                    type: 'line',
                    source: {
                        type: 'geojson',
                        data: {
                            type: 'Feature',
                            properties: {},
                            geometry: {
                                type: 'LineString',
                                coordinates: geojson
                            }
                        }
                    },
                    layout: {
                        'line-join': 'round',
                        'line-cap': 'round'
                    },
                    paint: {
                        'line-color': '#3887be',
                        'line-width': 5,
                        'line-opacity': 0.75
                    }
                });
            }
            // get the sidebar and add the instructions
            var instructions = document.getElementById('instructions');
            var steps = data.legs[0].steps;

            var tripInstructions = [];
            for (var i = 0; i < steps.length; i++) {
                tripInstructions.push('<br><li>' + steps[i].maneuver.instruction) + '</li>';
                instructions.innerHTML = '<br><span class="duration">Trip duration: ' + Math.floor(data.duration / 60) + ' min  </span>' + tripInstructions;
            }
        };
        req.send();
    }
    map.on('load', function () {
        // make an initial directions request that
        // starts and ends at the same location
        getRoute(start);

        // Add starting point to the map
        map.addLayer({
            id: 'point',
            type: 'circle',
            source: {
                type: 'geojson',
                data: {
                    type: 'FeatureCollection',
                    features: [{
                        type: 'Feature',
                        properties: {},
                        geometry: {
                            type: 'Point',
                            coordinates: start
                        }
                    }
                    ]
                }
            },
            paint: {
                'circle-radius': 10,
                'circle-color': '#3887be'
            }
        });

        map.addLayer({
            "id": "places",
            "type": "symbol",
            "source": {
                "type": "geojson",
                "data": {
                    "type": "FeatureCollection",
                    "features": data
                }
            },
            "layout": {
                "icon-image": "{icon}",
                "icon-allow-overlap": true
            }
        });

        map.addControl(new mapboxgl.NavigationControl());
        map.addControl(new mapboxgl.FullscreenControl());
        map.addControl(
            new mapboxgl.GeolocateControl({
                positionOptions: {
                    enableHighAccuracy: true
                },
                trackUserLocation: true
            })
        );
        // When a click event occurs on a feature in the places layer, open a popup at the
        // location of the feature, with description HTML from its properties.
        map.on('click', 'places', function (e) {
            var coordinates = e.features[0].geometry.coordinates.slice();
            var Address = e.features[0].properties.Address;
            // Ensure that if the map is zoomed out such that multiple
            // copies of the feature are visible, the popup appears
            // over the copy being pointed to.
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }
            new mapboxgl.Popup()
                .setLngLat(coordinates)
                .setHTML(Address)
                .addTo(map);
        });
        // Change the cursor to a pointer when the mouse is over the places layer.
        map.on('mouseenter', 'places', function () {
            map.getCanvas().style.cursor = 'pointer';
        });
        // Change it back to a pointer when it leaves.
        map.on('mouseleave', 'places', function () {
            map.getCanvas().style.cursor = '';
        });

        map.on('click', function (e) {
            var coordsObj = e.lngLat;
            canvas.style.cursor = '';
            var coords = Object.keys(coordsObj).map(function (key) {
                return coordsObj[key];
            });
            var end = {
                type: 'FeatureCollection',
                features: [{
                    type: 'Feature',
                    properties: {},
                    geometry: {
                        type: 'Point',
                        coordinates: coords
                    }
                }
                ]
            };
            if (map.getLayer('end')) {
                map.getSource('end').setData(end);
            } else {
                map.addLayer({
                    id: 'end',
                    type: 'circle',
                    source: {
                        type: 'geojson',
                        data: {
                            type: 'FeatureCollection',
                            features: [{
                                type: 'Feature',
                                properties: {},
                                geometry: {
                                    type: 'Point',
                                    coordinates: coords
                                }
                            }]
                        }
                    },
                    paint: {
                        'circle-radius': 10,
                        'circle-color': '#f30'
                    }
                });
            }
            getRoute(coords);
        });
    });


}
