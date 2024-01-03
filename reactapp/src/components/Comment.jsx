import './Comment.css'
import Map from './map.jsx'

function Comment(props) {
    return (
        <div>
            <hr/>
            <h4 className="comment-username">{props.userName}</h4>
            <div className="comment-container">
                <div className="comment-image-div">
                    <img className="comment-image" src={"data:image/png;base64," + props.photoCom} />
                </div>
                <div className="comment-location-div">
                    <Map latitude={props.lat} longitude={props.lon} display_name={props.locName} />
                </div>
                <div className="comment-info-div">
                    <p className="user-info">Komentar:</p>
                    <p className="comment-text">{props.textCom}</p>
                    <p className="user-info">Kontakt informacije:</p>
                    <p >{props.phoneNum}</p>
                    <p >{props.email}</p>
                </div>
            </div>
        </div>
    );
}

export default Comment;