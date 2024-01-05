import { useEffect, useState, useContext } from "react";
import "./Ad_detail.css";
import ListGroup from "./ListGroup";
import { useParams } from "react-router-dom";
import Map from './Map.jsx';
import { AuthContext } from "./AuthenticationContext";
import { useNavigate } from 'react-router-dom'
import NewComment from './NewComment.jsx'
import Comment from './Comment.jsx'
  

function Ad_detail() {

    let keyCounter = 0;

    const params = useParams();

    const navigate = useNavigate()

    const { user, updateUser } = useContext(AuthContext)

    const [isPending, setPending] = useState(true);
    const [addButton, setAddButton] = useState(true);
    const [addComment, setAddComment] = useState(false);
    const [colors, setColors] = useState('');
    const [images, setImages] = useState([]);
    const [firstImage, setFirstImage] = useState('');

    const [comments, setComments] = useState(false);

    const [the_ad, setTheAd] = useState([
        {
            adID: "",
            age: "",
            catAd: "",
            color: "",
            colorID: "",
            dateHourMis: "",
            description: "",
            location: "",
            lat: "",
            lon: "",
            namePet: "",
            petID: "",
            photo: "",
            photoID: "",
            species: "",
            userID: "",
            email: "",
            phoneNum: ""
        },
    ]);

    useEffect(() => {
        fetch('main/frontpagedata')
            .then(res => {
                return res.json();
            })
            .then(data => {
                const colors_arr = [];
                const images_arr = [];
                var color_string = '';
                var flag1 = 0;
                const findAd = data.Data.filter((ad) => ad.adID == params.id);
                setTheAd(findAd[0]);
                findAd.map((element) => {
                    if (!colors_arr.includes(element.color)) {
                        colors_arr.push(element.color);
                        if (flag1 == 0) {
                            flag1 = 1;
                            color_string += element.color;
                        } else {
                            color_string += ", " + element.color;
                        }
                    }
                    if (!images_arr.includes(element.photo)) {
                        images_arr.push(element.photo);
                    }
                })
                setFirstImage(images_arr[0]);
                images_arr.shift();
                setColors(color_string);
                setImages(images_arr);
                window.scrollTo(0, 0);
                return findAd[0];
            }).then(() => fetch('main/comment_data?adID=' + params.id)).then(res => {
                return res.json();
            }).then(data => {
                if (data.Data.length != 0) {
                    asyncSet(data.Data);
                } else {
                    setPending(false);
                }
                
            })
    }, []);

    async function asyncSet(data) {
        const result = await fetchCords(data);
        setComments([...result]);
    }

    async function fetchCords(data) {
        let locationsComment = [];

        const promises = data.map(async (comment) => {
            try {
                const url = `https://nominatim.openstreetmap.org/search?city='${comment.locCom}'&format=json&limit=1`;
                const response = await fetch(url, {
                    method: "GET",
                    mode: "cors",
                });
                if (response.ok) {
                    const data = await response.json();

                    const locationsObject = {
                        adID: comment.adID,
                        email: comment.email,
                        locCom: comment.locCom,
                        phoneNum: comment.phoneNum,
                        photoCom: comment.photoCom,
                        textCom: comment.textCom,
                        userName: comment.userName,
                        latitude: data[0].lat,
                        longitude: data[0].lon,
                    };

                    locationsComment.push(locationsObject);
                } else {
                    throw new Error("Network response was not ok");
                }
            } catch (error) {
                console.error("Error fetching data:", error);
                alert("Please check your input");
            }
        });
        await Promise.all(promises);
        setPending(false);
        return locationsComment;
    }

    function changeCommentState() {
        setAddButton(true);
        setAddComment(false);
    }
    function checkUserAuth() {
        if (!user.isAuth) {
            navigate("/login")
        }
    }

    return (
        <div className="home-detail-container">
            <div className="left-detail-categories">
                <h1 className="search-heading">Pretraživanje</h1>
                <div className="categories-detail-container">
                    <ListGroup />
                </div>
            </div>
            <div className="ads-detail-container">
                <div className="pet-image-container">
                    <div id="carouselExample" className="carousel slide">
                        <div className="carousel-inner">
                            <div className="carousel-item active" key={keyCounter}>
                                <img src={"data:image/png;base64," + firstImage} className="d-block w-100" alt="..." />
                            </div>
                            {images && images.map((image, keyCounter) => (
                                <div className="carousel-item" key={keyCounter+1}>
                                    <img src={"data:image/png;base64,"+image} className="d-block w-100" alt="..." />
                                </div>
                            ))}
                        </div>
                        <button className="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
                            <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span className="visually-hidden">Previous</span>
                        </button>
                        <button className="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
                            <span className="carousel-control-next-icon" aria-hidden="true"></span>
                            <span className
                                ="visually-hidden">Next</span>
                        </button>
                    </div>
                </div>
                <div className="pet-info-container">
                    <div className="pet-info-container-left">
                        <h2>{the_ad.namePet}</h2>
                        <p>
                            <i className="category-style">Vrsta: </i>
                            {the_ad.species}
                        </p>
                        <p>
                            <i className="category-style">Datum i vrijeme nestanka: </i>
                            {the_ad.dateHourMis}
                        </p>
                        <p>
                            <i className="category-style">Boja ljubimca: </i>
                            {colors}
                        </p>
                        <p>
                            <i className="category-style">Starost ljubimca: </i>
                            {the_ad.age}
                        </p>
                        <p>
                            <i className="category-style">Kategorija: </i>
                            {the_ad.catAd}
                        </p>
                        <p>
                            <i className="category-style">Opis: </i>
                            {the_ad.description}
                        </p>
                    </div>
                    <div className="pet-info-container-right">
                        <p>
                            <i className="category-style">Lokacija nestanka:</i>
                        </p>
                        <Map latitude={the_ad.lat} longitude={the_ad.lon} display_name={the_ad.location} />
                        <p>
                            <i className="category-style">Kontakt</i>
                        </p>
                        <p>
                            <i className="category-style">email: </i>
                            {the_ad.email}
                        </p>
                        <p>
                            <i className="category-style">Broj mobitela: </i>
                            {the_ad.phoneNum}
                        </p>
                    </div>
                </div>
                <div className="comment-section-container">
                    <h1>Komentari</h1>
                    <hr />
                    {isPending && <p>Učitavanje komentara...</p> }
                    {(!comments && !isPending) && <p>Nema komentara</p>}
                    {addButton && <button className="btn btn-light" id="add-button" onClick={() => { setAddComment(true); setAddButton(false); checkUserAuth(); }}>
                        Dodaj komentar <i className="bi bi-plus-lg"></i>
                    </button>}
                    {(addComment && user.isAuth) && < NewComment username={user.firstName + ' ' + user.lastName} change={changeCommentState} />}
                    {comments && comments.map((comment,keyCounter) => (
                        <Comment
                            userName={comment.userName}
                            textCom={comment.textCom}
                            photoCom={comment.photoCom}
                            phoneNum={comment.phoneNum}
                            email={comment.email}
                            lat={comment.latitude}
                            lon={comment.longitude}
                            locName={comment.locCom}
                            key={keyCounter + 1}
                        />
                    ))}
                </div>
            </div>
        </div>
    );
}

export default Ad_detail;
