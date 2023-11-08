import "./Ad_detail.css";
import ListGroup from "./ListGroup";
import { useParams } from "react-router-dom";

function Ad_detail() {
  const params = useParams();
  console.log(params);

  const the_ad = [];

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
          <img className="pet-image" src={the_ad.image} />
        </div>
        <div className="pet-info-container">
          <div className="pet-info-container-left">
            <h2>{the_ad.petname}</h2>
            <p>Vrsta: {the_ad.petspecies}</p>
            <p>Datum i vrijeme nestanka: {the_ad.datehour}</p>
            <p>Boja ljubimca: {the_ad.color}</p>
            <p>Starost ljubimca: {the_ad.age}</p>
            <p>Kategorija: {the_ad.kategorija}</p>
            <p>Opis: {the_ad.description}</p>
          </div>
          <div className="pet-info-container-right">
            <p>Lokacija nestanka:</p>
            <iframe
              width="250"
              height="200"
              src="https://www.openstreetmap.org/export/embed.html?bbox=15.191345214843752%2C45.45627757127799%2C16.476745605468754%2C46.12560451043768&amp;layer=mapnik"
              style={{ border: "0" }}
            ></iframe>
            <p>Kontakt</p>
            <p>email:{the_ad.email}</p>
            <p>mob:{the_ad.phonenumber}</p>
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
