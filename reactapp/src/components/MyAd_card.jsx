import "./MyAd_card.css";
import { useNavigate, Link } from 'react-router-dom'

function MyAd_card(props) {

    const navigate = useNavigate();
    
    const handleDelete = (event, id) => {
        fetch('main/deleteAd', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(JSON.stringify(id))
        })
            .then((res) => {
                if (res.ok) {
                    navigate("/moji-oglasi");
                }
            }) 
    };

    function handleEdit() {
        console.log("HandleEdit");
        navigate('updateAd', {state: props.data})
    }
    

  return (
    <div className="myad-card-container">
      <div className="myad-img">
              <img src={"data:image/png;base64,"+props.image} />
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
              <button onClick={handleEdit }>
          <i className="bi bi-pencil-fill" title="Uredi oglas"></i>
        </button>
        <button onClick={event => handleDelete(event, props.adID)}>
          <i className="bi bi-trash3-fill" title="Obriši oglas"></i>
        </button>
      </div>
    </div>
  );
}

export default MyAd_card;
