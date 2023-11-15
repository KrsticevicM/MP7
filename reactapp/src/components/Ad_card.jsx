import './Ad_card.css'

function Ad_card(props) {
    return (
        <div className="card-wrapper">
            <div className="card-top">
                <img className="image" src={"data:image/jpeg;base64,"+props.image} />
            </div>
            <div className="card-bottom">
                <span className="top-text">{props.petname}</span>
                <br />
                <span className="bottom-text">{props.description}</span>
                <br />
            </div>
        </div>
    );
  }
  
export default Ad_card;