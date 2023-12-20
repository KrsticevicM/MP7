import { TileLayer, MapContainer, Marker, Popup } from "react-leaflet"
import "leaflet/dist/leaflet.css";

function Map(props) {
    return (
        <MapContainer center={[44.515399, 16]} zoom={5.4} scrollWheelZoom={false}>

            <TileLayer
                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <Marker position={[props.latitude, props.longitude]}>
                <Popup>
                    {props.display_name}
                </Popup>
            </Marker> 
        </MapContainer>
    );
}

export default Map;