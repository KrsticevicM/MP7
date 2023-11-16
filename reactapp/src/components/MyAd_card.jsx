import "./MyAd_card.css";


function MyAd_card(props) {
  return (
    <div className="myad-card-container">
      <div className="myad-img">
        <img src={props.image} />
      </div>
      <div className="myad-info">
        <h1>{props.petname}</h1>
        <p>
          <span className="text_style">Izgubljen: </span>
          {props.datehour}
        </p>
        <p>
          <span className="text_style">Starost: </span>
          {props.age}
        </p>
        <p>
          <span className="text_style">Kategorija: </span>
          {props.kategorija}
        </p>
      </div>
      <div className="myad-commands">
        <button>
          <i className="bi bi-pencil-fill" title="Uredi oglas"></i>
        </button>
        <button>
          <i className="bi bi-trash3-fill" title="Obriši oglas"></i>
        </button>
      </div>
    </div>
  );
}

export default MyAd_card;
