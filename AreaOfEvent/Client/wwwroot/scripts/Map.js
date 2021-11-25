function onMapClicked(dotnetRef, e) {
  dotnetRef.invokeMethodAsync("OnMapInteract", e.latlng["lat"], e.latlng["lng"])
}

window.createMap = (dotnetRef, id, lat, lng) => {
  if (window.mapViews == null) {
    window.mapViews = {};
  }

  window.mapViews[id] = {
    map: L.map(id).setView([lat, lng], 13),
    marker: null
  }

  //pk.eyJ1IjoibHVrYXNuZXVob2xkIiwiYSI6ImNrdGVlZWZseDB5MHcyd3J3dHR1Y29oZHgifQ.duwEJBJcmMtXpekzhhUKoQ

  L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibHVrYXNuZXVob2xkIiwiYSI6ImNrdGVlZWZseDB5MHcyd3J3dHR1Y29oZHgifQ.duwEJBJcmMtXpekzhhUKoQ', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    maxZoom: 18,
    id: 'mapbox/streets-v11',
    tileSize: 512,
    zoomOffset: -1,
    //accessToken: 'pk.eyJ1IjoibHVrYXNuZXVob2xkIiwiYSI6ImNrdGVlZWZseDB5MHcyd3J3dHR1Y29oZHgifQ.duwEJBJcmMtXpekzhhUKoQ'
  }).addTo(window.mapViews[id].map);

  window.mapViews[id].map.on('click', (e) => onMapClicked(dotnetRef, e));
}

window.addMarkerToMap = (id, lat, lng) => {
  let mv = window.mapViews[id];

  if (!mv) {
    return;
  }

  if (mv.marker != null) {
    mv.marker.remove();
  }
  mv.marker = L.marker([lat, lng]);

  mv.marker.addTo(mv.map);
}

window.setViewOfMap = (id, lat, lng) => {
  let mv = window.mapViews[id];
  if (mv == null)
    return;
  mv.map.setView([lat, lng], 13);

}

window.disposeMap = (id) => {
  if (window.mapViews[id] != null) {
    window.mapViews[id].map = null;
    window.mapViews[id].marker = null;
  }
}