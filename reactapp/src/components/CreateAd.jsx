import React, { useEffect, useState } from 'react'
import { Form } from 'react-router-dom'
import "./CreateAd.css"

export const NewAd = () => {

  const [files,setFiles]=useState()
  const [preview,setPreview]=useState()

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
    "ostalo",
  ];

  const age = [
    "< 1 god.",
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

  const createAdSubmit=async(event)=>{
    event.preventDefault()
    //getting form data and turning it into object
    const data = new FormData(event.target)
    const submission={
        ime: data.get('ime'),
        vrsta: data.get('vrsta'),
        boja: data.get('boja'),
        godine: data.get('age'),
        datum: data.get('datum'),
        vrijeme: data.get('vrijeme'),
        opis: data.get('opis'),
        img: data.get("img")
    }
    console.log(submission)
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
                <img src={pic} className='createAd-img' />
            ))}
          </div>

          <div className="form-group">
            <label htmlFor="ime">Ime</label>
            <input type="text" 
            className="form-control" 
            id="ime" 
            name="ime" 
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
              <select className="form-control" id="age" name='age'>
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
              name='datum'></input>
          </div>
          <div className="form-group">
              <label htmlFor="vrijeme-nestanka">Vrijeme nestanka</label>
              <input 
              type="time" 
              className="form-control" 
              id="vrijeme-nestanka" 
              name='vrijeme'></input>
          </div>
          <div className="form-group">
              <label htmlFor="opis">Opis</label>
              <textarea 
              className="form-control" 
              id="opis" 
              rows="2"
              name='opis'></textarea>
          </div>
          <div className="adbutton">
              <button className='btn btn-primary'>Stvori oglas</button>
          </div>
            
        
        </Form>
    </div>
  )
}
