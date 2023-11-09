import { useEffect, useState } from "react";
import "./Ad_detail.css";
import ListGroup from "./ListGroup";
import { useParams } from "react-router-dom";

function Ad_detail() {
    const params = useParams();
    console.log(params);

    const [the_ad, setTheAd] = useState([
        {
            image: "",
            petname: "",
            description: "",
            id: "",
            petspecies: "",
            datehour: "",
            color: "",
            age: "",
            kategorija: "",
            email: "",
            phonenumber: "",
        },
    ]);

    const ads = [
        {
            image: "public/images/Bichon-frise-dog.webp",
            petname: "Johnny",
            description:
                "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Johnny",
            id: 1,
            petspecies: "Pas",
            datehour: "21.01.2003 16:42",
            color: "Bijela",
            age: "20 god.",
            kategorija: "U potrazi",
            email: "filip.smolic.zadar@gmail.com",
            phonenumber: "0989175125",
        },
        {
            image:
                "public/images/black-dog-breeds-black-labrador-retriever-1566497968.jpg",
            petname: "Timmy",
            description:
                "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Timmy",
            id: 2,
            petspecies: "Pas",
            datehour: "21.01.2003 16:42",
            color: "Bijela",
            age: "20 god.",
            kategorija: "U potrazi",
            email: "filip.smolic.zadar@gmail.com",
            phonenumber: "0989175125",
        },
        {
            image:
                "public/images/dog-puppy-on-garden-royalty-free-image-1586966191.jpg",
            petname: "Samuel",
            description:
                "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Samuel",
            id: 3,
            petspecies: "Pas",
            datehour: "21.01.2003 16:42",
            color: "Bijela",
            age: "20 god.",
            kategorija: "U potrazi",
            email: "filip.smolic.zadar@gmail.com",
            phonenumber: "0989175125",
        },
        {
            image: "public/images/gettyimages-1190158957-1040x690.jpg",
            petname: "Willy",
            description:
                "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Willy",
            id: 4,
            petspecies: "Pas",
            datehour: "21.01.2003 16:42",
            color: "Bijela",
            age: "20 god.",
            kategorija: "U potrazi",
            email: "filip.smolic.zadar@gmail.com",
            phonenumber: "0989175125",
        },
        {
            image: "public/images/preuzmi.jpg",
            petname: "Armando",
            description:
                "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Armando",
            id: 5,
            petspecies: "Pas",
            datehour: "21.01.2003 16:42",
            color: "Bijela",
            age: "20 god.",
            kategorija: "U potrazi",
            email: "filip.smolic.zadar@gmail.com",
            phonenumber: "0989175125",
        },
        {
            image: "public/images/images (1).jpg",
            petname: "Cillian",
            description:
                "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Cillian",
            id: 6,
            petspecies: "Pas",
            datehour: "21.01.2003 16:42",
            color: "Bijela",
            age: "20 god.",
            kategorija: "U potrazi",
            email: "filip.smolic.zadar@gmail.com",
            phonenumber: "0989175125",
        },
        {
            image: "public/images/PomeranianKardashianpoms.jpg",
            petname: "Rocky",
            description:
                "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Rocky",
            id: 7,
            petspecies: "Pas",
            datehour: "21.01.2003 16:42",
            color: "Bijela",
            age: "20 god.",
            kategorija: "U potrazi",
            email: "filip.smolic.zadar@gmail.com",
            phonenumber: "0989175125",
        },
        {
            image: "public/images/images.jpg",
            petname: "Jimmy",
            description:
                "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Jimmy",
            id: 8,
            petspecies: "Pas",
            datehour: "21.01.2003 16:42",
            color: "Bijela",
            age: "20 god.",
            kategorija: "U potrazi",
            email: "filip.smolic.zadar@gmail.com",
            phonenumber: "0989175125",
        },
        {
            image: "public/images/Portrait-of-a-brown-lagotto-romagnolo.jpg",
            petname: "Pippo",
            description:
                "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Pippo",
            id: 9,
            petspecies: "Pas",
            datehour: "21.01.2003 16:42",
            color: "Bijela",
            age: "20 god.",
            kategorija: "U potrazi",
            email: "filip.smolic.zadar@gmail.com",
            phonenumber: "0989175125",
        },
    ];

    function ShowData() {
        const findAd = ads.filter((ad) => ad.id == params.id);
        console.log(findAd[0].image);
        setTheAd(findAd);
    }

    useEffect(() => {
        ShowData();
        window.scrollTo(0, 0);
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
                    <img className="pet-image" src={the_ad[0].image} />
                </div>
                <div className="pet-info-container">
                    <div className="pet-info-container-left">
                        <h2>{the_ad[0].petname}</h2>
                        <p>
                            <i className="category-style">Vrsta: </i>
                            {the_ad[0].petspecies}
                        </p>
                        <p>
                            <i className="category-style">Datum i vrijeme nestanka: </i>
                            {the_ad[0].datehour}
                        </p>
                        <p>
                            <i className="category-style">Boja ljubimca: </i>
                            {the_ad[0].color}
                        </p>
                        <p>
                            <i className="category-style">Starost ljubimca: </i>
                            {the_ad[0].age}
                        </p>
                        <p>
                            <i className="category-style">Kategorija: </i>
                            {the_ad[0].kategorija}
                        </p>
                        <p>
                            <i className="category-style">Opis: </i>
                            {the_ad[0].description}
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
                            {the_ad[0].email}
                        </p>
                        <p>
                            <i className="category-style">mob: </i>
                            {the_ad[0].phonenumber}
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
