import './Ad_card.css'
import React from 'react';

function Ad_card(props) {
    return (
      <div className="card-wrapper">
      <div className="card-top">
      <img className="image" src={props.image}/>
      </div>
      <div className="card-bottom">
      <span className="top-text">{props.name}</span><br/>
      <span className="bottom-text">Izgubljen 12.8.2021, aktivno se traži, odaziva se i na puno ime Johnattan</span>
      <br/>
      </div>
      </div>
    );
  }
  
export default Ad_card;