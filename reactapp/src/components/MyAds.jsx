import MyAd_card from "./MyAd_card";
import "./MyAds.css";
import { Link } from "react-router-dom";

function MyAds() {
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
      age: "2 god.",
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
      datehour: "4.8.2022 4:20",
      color: "Crna",
      age: "7 god.",
      kategorija: "U potrazi",
      email: "filip.smolic.zadar@gmail.com",
      phonenumber: "0989175125",
    },
  ];

  return (
    <div className="myads-container">
      <br />
      {ads.map((ad) => (
        <MyAd_card
          key={ad.id}
          image={ad.image}
          petname={ad.petname}
          datehour={ad.datehour}
          age={ad.age}
          kategorija={ad.kategorija}
        />
      ))}
      <Link to="new_ad">
        <button className="btn btn-light" id="add-button">
          Dodaj oglas <i className="bi bi-plus-lg"></i>
        </button>
      </Link>
    </div>
  );
}

export default MyAds;
