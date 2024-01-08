import { useState } from 'react';
import { MapContainer, Marker, Popup, TileLayer, useMapEvents } from 'react-leaflet'

function AddMarkerToClick(props) {

  const [marker, setMarker] = useState({lat:33,lng:33});

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
        <Marker position={marker}>
            <Popup>Marker is at</Popup>
        </Marker>
      
    </>
  )
}

export default AddMarkerToClick