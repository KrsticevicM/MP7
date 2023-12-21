import { useEffect, useState } from "react";
import "./Ad_detail.css";
import ListGroup from "./ListGroup";
import { useParams } from "react-router-dom";
import Map from './Map.jsx';
  

function Ad_detail() {
    const params = useParams();

    const [colors, setColors] = useState('');
    const [images, setImages] = useState([]);
    const [firstImage, setFirstImage] = useState('');
    const [location, setLocation] = useState({
        latitude: 0,
        longitude: 0,
        display_name: "",
    });

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
            }).then(data => {
                let url = `https://nominatim.openstreetmap.org/search?city='${data.location}'&format=json&limit=1`;
                fetch(url, {
                    method: "GET",
                    mode: "cors",
                })
                    .then((response) => {
                        if (response.ok) {
                            return response.json();
                        }
                    })
                    .then((data) => {

                        setLocation({
                            latitude: data[0].lat,
                            longitude: data[0].lon,
                            display_name: data[0].display_name,
                        })
                    }).catch(() => alert("Please Check your input"));
            });
    }, []);

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
                            <div className="carousel-item active" key="image">
                                <img src={"data:image/png;base64," + firstImage} className="d-block w-100" alt="..." />
                            </div>
                            {images && images.map((image) => (
                                <div className="carousel-item" key="image">
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
                    <p>Nema komentara</p>
                </div>
            </div>
        </div>
    );
}

export default Ad_detail;
