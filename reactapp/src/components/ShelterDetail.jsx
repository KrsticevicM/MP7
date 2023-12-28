import React from 'react'
import "./ShelterDetail.css"
import { useLoaderData, useParams } from 'react-router-dom'

function ShelterDetail() {

  const data = useLoaderData()
  const {id} = useParams()
  
  console.log(data)

  return (

    <div className='shelter-container'>
        <h1>ShelterDetail {id} </h1>
        
    </div>
    
  )
}
export const DetailLoader = async () =>{
  
  const res= await fetch("main/shelter_data")
  return res.json()
}

export default ShelterDetail