import { useState } from 'react';
import { MapContainer, Marker, Popup, TileLayer, useMapEvents } from 'react-leaflet'

function AddMarkerToClick(props) {

  const [marker, setMarker] = useState({lat:null,lng:null});

  const map = useMapEvents({
    click(e) {
      const newMarker = e.latlng
      setMarker(newMarker);
      props.onClick(newMarker)
    },
  })

  return (
    <>
        <TileLayer 
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        />
        {marker.lat && marker.lng &&
        <Marker position={marker}>
            <Popup>Mjesto nestanka</Popup>
        </Marker>}
      
    </>
  )
}

export default AddMarkerToClick