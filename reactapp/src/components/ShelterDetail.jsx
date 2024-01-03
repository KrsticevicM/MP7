import React, { useEffect, useState } from 'react'
import "./ShelterDetail.css"
import { Link, useLoaderData, useParams } from 'react-router-dom'
import Ad_card from './Ad_card'

function ShelterDetail() {

  const data = useLoaderData()
  const {id} = useParams()

  const shelterData=data.ShelterDetail.filter(shelter=>{ 
    return shelter.userID==id
  })[0]
  
  const update_ads = [];
  const ad_ids = []
  data.Ads.Data.map((ad) => {
    if (!(ad_ids.includes(ad.adID))) {
      update_ads.push(ad);
      ad_ids.push(ad.adID);   
    }
  })
  
  const shelterAds=update_ads.filter(ad=>{
    return ad.userID==id
  })
    
  

  return (

    <div className='shelter-container'>
        <h1>{shelterData.nameShelter}</h1>
        <div>Email: {shelterData.email}</div>
        <div>Broj telefona: {shelterData.phoneNum}</div>
        
        <div className="ads-container">
          <div className="ads-container2">
            {shelterAds && shelterAds.map((ad) => (
              <Link to={'/'+ad.adID} key={ad.adID}>
                <Ad_card
                  petname={ad.namePet}
                  image={ad.photo}
                  description={ad.description}
                />
              </Link>
            ))}
          </div>
        </div> 
    </div>
    
  )
}
export const DetailLoader = async () =>{
  
  const shelter= await fetch("/main/shelter_data")
  .then(res=>{
    return res.json()
  })

  const AdData= await fetch("/main/frontpagedata")
  .then(res=>{
    return res.json()
  })

  return {"ShelterDetail":shelter, "Ads":AdData}
  
}

export default ShelterDetail