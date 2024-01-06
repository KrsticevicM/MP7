import './NewComment.css'
import { useState, useContext } from 'react'
import { Form, useNavigate, useParams } from 'react-router-dom'
import { AuthContext } from "./AuthenticationContext";


function NewComment(props) {

    const params = useParams();

    const navigate = useNavigate();

    const { user, updateUser } = useContext(AuthContext);
    const [preview, setPreview] = useState()
    const [file, setFile] = useState()
    const [error, setError] = useState("")

    function handleChange(e) {
        setFile(e.target.files[0]);
        setPreview(URL.createObjectURL(e.target.files[0]));
    }

    function removeImage() {
        setPreview();
        setFile();
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
                "locCom": data.get('location-city')
            }]
        }
        console.log(submission)
        fetch('main/postComment', {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ submission })
        }).then((res) => {
            console.log(JSON.stringify(submission))
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