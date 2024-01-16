import './NewComment.css'
import { useState, useContext } from 'react'
import { Form, useNavigate, useParams } from 'react-router-dom'
import { AuthContext } from "./AuthenticationContext";
import { MapContainer, useMapEvents } from 'react-leaflet';
import AddMarkerToClick from './AddMarkerToClick.jsx';


function NewComment(props) {

    const params = useParams();

    const navigate = useNavigate();

    const { user, updateUser } = useContext(AuthContext);
    const [preview, setPreview] = useState()
    const [file, setFile] = useState()
    const [error, setError] = useState("")
    const [lat, setLat] = useState();
    const [lng, setLng] = useState();

    function handleChange(e) {
        setFile(e.target.files[0]);
        setPreview(URL.createObjectURL(e.target.files[0]));
    }

    function removeImage() {
        setPreview();
        setFile();
    }

    const getLatLng = (data) => {
        setLat(data.lat)
        setLng(data.lng)
    }

    const getBase64 = (file) => {
        return new Promise((resolve, reject) => {
            const reader = new FileReader()
            reader.readAsDataURL(file)
            reader.onload = () => {
                resolve(reader.result)
            }
            reader.onerror = reject
        })
    }

    const createAdSubmit = async (event) => {
        event.preventDefault()
        //getting form data and turning it into object
        const data = new FormData(event.target)
        let images2 = ""
        setError("")
        if (!file) {
            setError("Stavite sliku")
            return
        }

        const base64 = await getBase64(file) // `file` your img file
        images2 = base64.split(",").pop()

        const submission = {
            "Data": [{
                "userID": user.userID,
                "adID": params.id,
                "photoCom": images2,
                "textCom": data.get('comment-text'),
                "lat": lat,
                "lon": lng,
            }]
        }

        console.log(submission);
       
        
        fetch(`main/post_comment`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(JSON.stringify(submission))
        }).then((res) => {
            //setIsPending(false)
            if (res.ok) {
                navigate("/" + params.id);
            }
        })

    }

    return (
        <>
            <h4 className="comment-user">{props.username.toLowerCase()}</h4>
            <Form onSubmit={createAdSubmit}>
            <div className="newcomment-container">
                <div className="form-floating" id="comment-text">
                    <textarea className="form-control" placeholder="Leave a comment here" id="floatingTextarea2" name="comment-text"></textarea>
                    <label htmlFor="commment-text">Novi komentar</label>
                </div>
                <div className="comment-location">
                
                        <div className="map-container">
                            <MapContainer center={[44.515399, 16]} zoom={5.4} scrollWheelZoom={true} > {/* omit onClick */}
                                <AddMarkerToClick onClick={getLatLng} />
                            </MapContainer>
                        </div>
                </div>
                <div className="comment-image-upload">
                    {!preview && <input type="file" name="myImage" className="image-upload-button" onChange={handleChange} />}
                        {preview && <div className="img-container"><img src={preview} className='new-comment-image' /><i className="bi bi-trash3-fill" id="trashcan" onClick={removeImage}></i></div>}
                        {error && <p>{error}</p> }
                </div>
                <div className="comment-commands">
                    <button type="submit">
                        <i className="bi bi-send-fill"></i>
                    </button>
                    <button id="comment-cancel-button" onClick={() => props.change()}>
                        <i className="bi bi-x-lg"></i>
                    </button>
                    </div>
                </div>
            </Form>
        </>
    );
}

export default NewComment;