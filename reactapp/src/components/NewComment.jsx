import './NewComment.css'

function NewComment(props) {
    return (
        <>
        <h4 className="comment-user">{props.username.toLowerCase()}</h4>
        <div className="newcomment-container">
                <div className="form-floating" id="comment-text">
                    <textarea className="form-control" placeholder="Leave a comment here" id="floatingTextarea2" style={{ height: '100px' }}></textarea>
                    <label htmlFor="floatingTextarea2">Novi komentar</label>
                </div>
                <div className="comment-location">
                <div className="form-floating mb-3" id="comment-location-city">
                    <input
                        type="name"
                        className="form-control"
                        id="location-city-comment"
                        name="location-city"
                        placeholder="Grad nestanka"
                    />
                    <label htmlFor="comment-location-city">Grad nestanka</label>
                </div>
                <div className="form-floating mb-3" id="comment-location-street">
                    <input
                        type="name"
                        className="form-control"
                        id="location-street-comment"
                        name="location-street"
                        placeholder="Ulica nestanka"
                    />
                    <label htmlFor="comment-location-street">Ulica nestanka</label>
                    </div>
                </div>
                <div className="comment-commands">
                    <button>
                        <i className="bi bi-send-fill"></i>
                </button>
                <button id="comment-cancel-button" onClick={() => props.change()}>
                        <i className="bi bi-x-lg"></i>
                    </button>
                </div>
            </div>
        </>
    );
}

export default NewComment;