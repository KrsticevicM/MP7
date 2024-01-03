import './Comment.css'

function Comment(props) {
    return (
        <div>
            <hr/>
            <h4 className="comment-username">{props.userName}</h4>
            <p className="comment-text">{props.textCom}</p>
            <div className="comment-container">
                <div className="comment-image-div">
                    <img className="comment-image" src={"data:image/png;base64," + props.photoCom} />
                </div>
                <div className="comment-location-div"></div>
                <div className="comment-info-div">
                    <p className="user-info">Kontakt informacije:</p>
                    <p >{props.phoneNum}</p>
                    <p >{props.email}</p>
                </div>
            </div>
        </div>
    );
}

export default Comment;