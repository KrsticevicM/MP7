import React, { useContext, useEffect, useState } from 'react'
import { Form, Navigate, redirect, useNavigate } from 'react-router-dom'
import "./CreateAd.css"
import { AuthContext } from './AuthenticationContext'

export const NewAd = () => {

  const [files,setFiles]=useState()
  const [preview,setPreview]=useState()
  const {user,updateUser}= useContext(AuthContext)
  const [images,setImages]=useState("")
  const navigate=useNavigate()
  const [error,setError]=useState("")

  

  const species = [
    "Pas",
    "Mačka",
    "Ptica",
    "Glodavac",
    "Kunić",
    "Gmaz",
    "Ostalo",
  ];
  const colors = [
    "crna",
    "smeđa",
    "zelena",
    "siva",
    "crvena",
    "žuta",
    "narančasta",
    "ljubičasta",
    "plava",
    "bijela",
    "šarena",
  ];

  const age = [
    "<1 mj.",
    "1-6 mj.",
    "6-11 mj.",
    "1 god.",
    "2 god.",
    "3 god.",
    "4-5 god.",
    "6-10 god.",
    "> 10 god.",
  ];


  useEffect(()=>{

    
    if(!files) return;

    let tmp=[]
    for(let i = 0;i<files.length;i++){
      tmp.push(URL.createObjectURL(files[i]))
    }
    const objectUrls=tmp
    setPreview(objectUrls)
    for (let i=0; i< objectUrls.length;i++){
      return()=>{
        URL.revokeObjectURL(objectUrls[i])
      }
    }

    
  },[files])

  
  
  const getBase64=(file)=> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader()
      reader.readAsDataURL(file)
      reader.onload = () => {
        resolve(reader.result)
      }
      reader.onerror = reject
    })
  }
  
  // usage
  




  const createAdSubmit=async(event)=>{
    event.preventDefault()
    //getting form data and turning it into object
    const data = new FormData(event.target)
    let images2=""
    setError("")
    if(!files){
      setError("Stavite barem jednu sliku")
      return
    }
    if(files.length>3){
      setError("Maksimalno 3 slike su dopuštene")
      return
    }
    

    const base64=await getBase64(files[0]) // `file` your img file
    images2=base64.split(",").pop()

    if(files.length>=2){
      const base6423=await getBase64(files[1]) // `file` your img file
      images2=images2+","+base6423.split(",").pop()
    }

    if(files.length==3){
      const base6424=await getBase64(files[2]) // `file` your img file
      images2=images2+","+base6424.split(",").pop()
    }
    


    const submission={
        namePet: data.get('ime'),
        species: data.get('vrsta'),
        color: data.get('boja'),
        age: data.get('age'),
        description: data.get('opis'),
        catAd:"u potrazi",
        location:"Zagreb",
        userID: user.userID,
        dateHourMis: data.get('datum') + " "+data.get('vrijeme'),
        img: images2 
    }
    console.log(submission)
    /* fetch("main/newAd",{
      method: "POST",
      headers: {"Content-Type": "application/json"},
      body: JSON.stringify(submission)
    }).then(()=>{
      console.log(submission)
      //setIsPending(false)
        
    }) */
    navigate("/moji-oglasi")
  }
  
  return (

    <div className='createAd-container'>
        <h1 className='createAd-headline'>Novi Oglas</h1>
        <Form className='createAd-window' onSubmit={createAdSubmit}>
          <div className="form-group">
            <label htmlFor="img">Slika</label>
            <input
                type='file'
                accept='image/*'
                multiple
                id='img'
                name='img'
                onChange={(e)=>{
                setFiles(e.target.files)
                }}
            />
            {preview && preview.map((pic)=>(
                <img src={pic} key={pic} className='createAd-img' />
            ))}
          </div>

          <div className="form-group">
            <label htmlFor="ime">Ime</label>
            <input type="text" 
            className="form-control" 
            id="ime" 
            name="ime" 
            required
            placeholder="Ime na koje se odziva"/>
          </div>

          <div className="form-group">
              <label htmlFor="vrsta">Vrsta životinje</label>
              <select className="form-control" id="vrsta" name='vrsta'>
              {species.map((specie) => (
              <option key={specie} value={specie}>
                  {specie}
              </option>
              ))}
              </select>
          </div>

          <div className="form-group">
              <label htmlFor="boja">Boja</label>
              <select className="form-control" id="boja" name='boja'>
              {colors.map((specie) => (
              <option key={specie} value={specie}>
                  {specie}
              </option>
              ))}
              </select>
          </div>

          <div className="form-group">
              <label htmlFor="age">Starost</label>
              <select className="form-control" id="age" name='age' maxmenuheight={20} menuplacement="auto" >
              {age.map((specie) => (
              <option key={specie} value={specie}>
                  {specie}
              </option>
              ))}
              </select>
          </div>
          <div className="form-group">
              <label htmlFor="datum-nestanka">Datum nestanka</label>
              <input 
              type="date" 
              className="form-control" 
              id="datum-nestanka" 
              name='datum'
              required
              ></input>
          </div>
          <div className="form-group">
              <label htmlFor="vrijeme-nestanka">Vrijeme nestanka</label>
              <input 
              type="time" 
              className="form-control" 
              id="vrijeme-nestanka" 
              name='vrijeme'
              required
              ></input>
          </div>
          <div className="form-group">
              <label htmlFor="opis">Opis</label>
              <textarea 
              className="form-control" 
              id="opis" 
              rows="2"
              name='opis'
              required></textarea>
          </div>
          <div className="adbutton">
              <button className='btn btn-primary'>Stvori oglas</button>
          </div>
          
          {error.length!=0 && <div className='form-group'>
            <p className='error-message'>{error}</p>
            </div>}
            
        
        </Form>
    </div>
  )
}
