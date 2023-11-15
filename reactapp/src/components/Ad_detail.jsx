import { useEffect, useState } from "react";
import "./Ad_detail.css";
import ListGroup from "./ListGroup";
import { useParams } from "react-router-dom";

function Ad_detail() {
    const params = useParams();
    console.log(params);

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
            namePet: "",
            petID: "",
            photo: "",
            photoID: "",
            species: "",
            userID: "",
        },
    ]);

    useEffect(() => {
        fetch('main')
            .then(res => {
                return res.json();
            })
            .then(data => {
                console.log(data.Data);
                console.log(btoa(data.Data[0].photo));
                const findAd = data.Data.filter((ad) => ad.adID == params.id);
                setTheAd(findAd[0]);   
                window.scrollTo(0, 0);
            })
    }, []);

    return (
        <div className="home-container">
            <div className="left-categories">
                <h1 className="search-heading">Pretraživanje</h1>
                <div className="categories-container">
                    <ListGroup />
                </div>
            </div>
            <div className="ads-detail-container">
                <div className="pet-image-container">
                    <img className="pet-image" src={"data:image/png;base64,"+the_ad.photo} />
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
                            {the_ad.color}
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
                        <iframe
                            width="250"
                            height="200"
                            src="https://www.openstreetmap.org/export/embed.html?bbox=15.191345214843752%2C45.45627757127799%2C16.476745605468754%2C46.12560451043768&amp;layer=mapnik"
                            style={{ border: "0" }}
                        ></iframe>
                        <p>
                            <i className="category-style">Kontakt</i>
                        </p>
                        <p>
                            <i className="category-style">email: </i>
                            filip.smolic.zadar@gmail.com
                        </p>
                        <p>
                            <i className="category-style">mob: </i>
                            0989175125
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
