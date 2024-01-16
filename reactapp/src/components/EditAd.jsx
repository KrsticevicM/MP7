import React, { useContext, useEffect, useState } from 'react'
import { Form, Navigate, redirect, useNavigate, useLocation } from 'react-router-dom'
import "./CreateAd.css"
import { AuthContext } from './AuthenticationContext'
import Map from './Map.jsx';
import { MapContainer, useMapEvents } from 'react-leaflet';
import AddMarkerToClick from './AddMarkerToClick.jsx';
import { Marker, Popup } from "react-leaflet"

export const EditAd = () => {

    const [files, setFiles] = useState()
    const [preview, setPreview] = useState()
    const { user, updateUser } = useContext(AuthContext)
    const [images, setImages] = useState("")
    const navigate = useNavigate()
    const [error, setError] = useState("")
    const [lat, setLat] = useState()
    const [lng, setLng] = useState()
    const [color, setColor] = useState("")

    const { state } = useLocation();

    const [mapData, setMapData] = useState(false);

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
        "< 1 god.",
        "1 god.",
        "2 god.",
        "3 god.",
        "4-5 god.",
        "6-10 god.",
        ">10 god.",
    ];

    const categories = [
        "u potrazi",
        "sretno pronađen",
        "nije pronađen",
        "pronađen uz nesretne okolnosti"
    ]

    const getLatLng = (data) => {
        setLat(data.lat)
        setLng(data.lng)
    }

    const b64toBlob = (b64Data, contentType = '', sliceSize = 512) => {
        const byteCharacters = atob(b64Data);
        const byteArrays = [];

        for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            const slice = byteCharacters.slice(offset, offset + sliceSize);

            const byteNumbers = new Array(slice.length);
            for (let i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            const byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        const blob = new Blob(byteArrays, { type: contentType });
        return blob;
    }


    useEffect(() => {

        console.log(state);

        var speciesSelect = document.getElementById("vrsta");
        speciesSelect.options[species.indexOf(state.species)].selected = true;

        state.color_list.map(color => {
            document.getElementsByName(color + "-boja")[0].checked = true;
        })

        var ageSelect = document.getElementById("age");
        ageSelect.options[age.indexOf(state.age)].selected = true;

        var catSelect = document.getElementById("kategorija-oglasa");
        if (state.catAd == "u skloništu") {
            catSelect.options[4].selected = true;
        } else {
            catSelect.options[categories.indexOf(state.catAd)].selected = true;
        }

        var dateString = state.dateHourMis.split(' ')[0];
        dateString = dateString.split('.');
        const dt = new Date(dateString[2], dateString[1], dateString[0]);
        const day = ("0" + dt.getDate()).slice(-2);
        const month = ("0" + (dt.getMonth() + 1)).slice(-2);
        const date = dt.getFullYear() + "-" + month + "-" + day;
        document.getElementsByName("datum")[0].value = date;

        const mapDataSet = { lattitude: "", longitude: "", display_name:"" };
        mapDataSet.lattitude = state.lat;
        mapDataSet.longitude = state.lon;
        mapDataSet.display_name = state.location;
        setMapData(mapDataSet);

        var timeString = state.dateHourMis.split(' ')[1]
        if (timeString.length == 4 && timeString[0] != "0") {
            timeString = "0" + timeString;
        }
        document.getElementsByName("vrijeme")[0].value = timeString;

        const filesBlob = [];
        if (!files) {
            state.photo_list.map(photo => {
                filesBlob.push(b64toBlob(photo, "application / python - pickle"))
            });
            setFiles(filesBlob);
            return;
        }


        console.log(files);

        let tmp = []
        for (let i = 0; i < files.length; i++) {
            tmp.push(URL.createObjectURL(files[i]))
        }
        const objectUrls = tmp
        console.log(objectUrls);
        setPreview(objectUrls)
        for (let i = 0; i < objectUrls.length; i++) {
            return () => {
                URL.revokeObjectURL(objectUrls[i])
            }
        }


    }, [files])



    const getBase64 = (file) => {
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





    const createAdSubmit = async (event) => {
        event.preventDefault()
        //getting form data and turning it into object
        const data = new FormData(event.target)
        let images2 = ""
        setError("")
        if (!files) {
            setError("Stavite barem jednu sliku")
            return
        }
        if (files.length > 3) {
            setError("Maksimalno 3 slike su dopuštene")
            return
        }

        if (!lat && !lng) {
            setLat(state.lat);
            setLng(state.lon);
        }

        const base64 = await getBase64(files[0]) // `file` your img file
        images2 = base64.split(",").pop()

        if (files.length >= 2) {
            const base6423 = await getBase64(files[1]) // `file` your img file
            images2 = images2 + "," + base6423.split(",").pop()
        }

        if (files.length == 3) {
            const base6424 = await getBase64(files[2]) // `file` your img file
            images2 = images2 + "," + base6424.split(",").pop()
        }
        const date = data.get('datum')
        const y = date.slice(0, 4)
        const m = date.slice(5, 7)
        const d = date.slice(8, 10)
        let kategorija = data.get("kategorija-oglasa")

        if (kategorija == null) {
            kategorija = "u potrazi"
        }

        const boja = [data.get(colors[0] + '-boja'), data.get(colors[1] + '-boja'), data.get(colors[2] + '-boja'),
        data.get(colors[3] + '-boja'), data.get(colors[4] + '-boja'), data.get(colors[5] + '-boja'), data.get(colors[6] + '-boja'),
        data.get(colors[7] + '-boja'), data.get(colors[8] + '-boja'), data.get(colors[9] + '-boja'), data.get(colors[10] + '-boja')]


        const boje = boja.filter(boja => {
            return boja != null
        }).join(",")


        const submission = {
            "Data": [{
                "namePet": data.get('ime'),
                "species": data.get('vrsta'),
                "color": boje,
                "age": data.get('age'),
                "description": data.get('opis'),
                "catAd": kategorija,
                "userID": user.userID,
                "dateHourMis": d + "." + m + "." + y + "." + " " + data.get('vrijeme'),
                "img": images2,
                "lat": lat,
                "lon": lng,
                "location": data.get("grad-nestanka")
            }]
        }
        console.log(submission)
        fetch(`main/postAd`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(JSON.stringify(submission))
        }).then((res) => {
            console.log(submission)
            //setIsPending(false)
            if (res.ok) {
                navigate("/moji-oglasi")
            }
        })

    }

    return (

        <div className='createAd-container'>
            <h1 className='createAd-headline'>Uredi Oglas</h1>
            <Form className='createAd-window' onSubmit={createAdSubmit}>
                <div className="form-group">
                    <label htmlFor="img">Slika</label>
                    <input
                        type='file'
                        accept='image/*'
                        multiple
                        id='img'
                        name='img'
                        onChange={(e) => {
                            setFiles(e.target.files)
                        }}
                    />

                </div>
                <div className="form-group">
                    {preview && preview.map((pic) => (
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
                        placeholder="Ime na koje se odziva"
                        defaultValue={state.namePet }
                    />
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
                    <label htmlFor="kategorija-oglasa">Kategorija oglasa</label>
                    <select className="form-control" id="kategorija-oglasa" name='kategorija-oglasa'>
                        {categories.map((category) => (
                            <option key={category} value={category}>
                                {category}
                            </option>
                        ))}
                        {user.isShelter && <option value="u skloništu">
                            u skloništu
                        </option>}

                    </select>
                </div>

                {/* <div className="form-group">
              <label htmlFor="boja">Boja</label>
              <select className="form-control" id="boja" name='boja'>
              {colors.map((specie) => (
              <option key={specie} value={specie}>
                  {specie}
              </option>
              ))}
              </select>
          </div> */}
                <label htmlFor="color-block">Boja</label>
                <div className='color-block'>
                    <div className='myrow'>
                        <input type="checkbox" className="btn-check" id="btn-check-0-outlined" autoComplete="off" name={colors[0] + '-boja'} value={colors[0]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-0-outlined">{colors[0]}</label>

                        <input type="checkbox" className="btn-check" id="btn-check-1-outlined" autoComplete="off" name={colors[1] + '-boja'} value={colors[1]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-1-outlined">{colors[1]}</label>

                        <input type="checkbox" className="btn-check" id="btn-check-2-outlined" autoComplete="off" name={colors[2] + '-boja'} value={colors[2]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-2-outlined">{colors[2]}</label>

                        <input type="checkbox" className="btn-check" id="btn-check-3-outlined" autoComplete="off" name={colors[3] + '-boja'} value={colors[3]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-3-outlined">{colors[3]}</label>

                        <input type="checkbox" className="btn-check" id="btn-check-4-outlined" autoComplete="off" name={colors[4] + '-boja'} value={colors[4]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-4-outlined">{colors[4]}</label>

                        <input type="checkbox" className="btn-check" id="btn-check-5-outlined" autoComplete="off" name={colors[5] + '-boja'} value={colors[5]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-5-outlined">{colors[5]}</label>
                    </div>
                    <div className='myrow'>
                        <input type="checkbox" className="btn-check" id="btn-check-6-outlined" autoComplete="off" name={colors[6] + '-boja'} value={colors[6]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-6-outlined">{colors[6]}</label>

                        <input type="checkbox" className="btn-check" id="btn-check-7-outlined" autoComplete="off" name={colors[7] + '-boja'} value={colors[7]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-7-outlined">{colors[7]}</label>

                        <input type="checkbox" className="btn-check" id="btn-check-8-outlined" autoComplete="off" name={colors[8] + '-boja'} value={colors[8]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-8-outlined">{colors[8]}</label>

                        <input type="checkbox" className="btn-check" id="btn-check-9-outlined" autoComplete="off" name={colors[9] + '-boja'} value={colors[9]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-9-outlined">{colors[9]}</label>

                        <input type="checkbox" className="btn-check" id="btn-check-10-outlined" autoComplete="off" name={colors[10] + '-boja'} value={colors[10]} />
                        <label className="btn btn-outline-dark" htmlFor="btn-check-10-outlined">{colors[10]}</label>
                    </div>
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
                    <label htmlFor="grad-nestanka">Grad nestanka</label>
                    <input type="text"
                        className="form-control"
                        id="grad-nestanka"
                        name="grad-nestanka"
                        required
                        placeholder="Ime grada nestanka"
                        defaultValue={state.location }
                    />
                </div>
                <div className="map-container">
                    <MapContainer center={[44.515399, 16]} zoom={5.4} scrollWheelZoom={true} > {/* omit onClick */}
                        {(!lat && !lng && mapData) && <Marker position={[mapData.lattitude, mapData.longitude]}>
                            <Popup>
                                {mapData.display_name}
                            </Popup>
                        </Marker>}
                        <AddMarkerToClick onClick={getLatLng} />
                    </MapContainer>
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
                <div className="text-center">
                    <button className='btn btn-primary'>Spremi</button>
                </div>

                {error.length != 0 && <div className='form-group'>
                    <p className='error-message'>{error}</p>
                </div>}


            </Form>
        </div>
    )
}

export default EditAd;
