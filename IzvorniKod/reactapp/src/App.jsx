import './App.css'
import Home from './components/Home'
import {Route, createBrowserRouter, createRoutesFromElements, RouterProvider } from 'react-router-dom'
import Login from './components/Login'
import Registration from './components/Registration'
import { RootLayout } from './layouts/RootLayout'
import { NotFound } from './components/NotFound'
import Ad_detail from './components/Ad_detail'
import Shelter, { ShelterLoader } from './components/Shelter'
import InactiveAds from './components/InactiveAds'
import { AuthContext } from './components/AuthenticationContext'
import { useEffect, useState } from 'react'
import MyAds from './components/MyAds'
import { NewAd } from './components/CreateAd'
import {EditAd } from './components/EditAd'
import ShelterDetail, { DetailLoader } from './components/ShelterDetail'


const router=createBrowserRouter(
    createRoutesFromElements(
        
        <Route path="/" element={<RootLayout/>}>

            <Route index element={<Home />} />

            <Route path='login' element={<Login />}/>            

            <Route path="/sklonista" element={<Shelter />} loader={ShelterLoader} />

            <Route path="/neaktivni_oglasi" element={<InactiveAds />} />

            <Route exact path="/skloniste/:id" element={<ShelterDetail/>} loader={DetailLoader}/>

            <Route path="/moji-oglasi" element={<MyAds />}/>

            <Route path="newAd" element={<NewAd />} />

            <Route path="/updateAd" element={<EditAd />} />

            <Route path='registration' element={<Registration/>}/>

            <Route path=":id" element={<Ad_detail />}></Route>

            <Route path="*" element={<NotFound/>}/> {/*error page */}
            
            
        </Route>

        
    )
)



function App() {
    
    const [user, setUser] = useState({
        isAuth: false,
        isShelter: false,
        userID: null,
        firstName: '',
        lastName: '',
    })
    
    
    const updateUser = (newUserData) => {
        setUser((prevUser) => ({ ...prevUser, ...newUserData }));
    }

    useEffect(()=>{
        if(localStorage.getItem("loginValue") && user.isAuth==false){
        
            fetch(`main/auth?id=${localStorage.getItem("loginValue")}`
            ).then((res)=>{
                
                return res.text()
            
            }).then(text=>{
                
                text = JSON.parse(text)
                text = text.Data[0]
                console.log(text)
                if (!text.firstName){
                    text.firstName = text.nameShelter
                    updateUser({isShelter: true})
                }
                if (!text.lastName){
                    text.lastName = ""
                }
                updateUser({
                    userID: text.userID,
                    isAuth:true, 
                    firstName: text.firstName.toUpperCase(), 
                    lastName: text.lastName.toUpperCase()
                })
            }).catch(err=>{
                console.log(err.message)
                localStorage.removeItem("loginValue")
            }) 
        }


    },[])
    

    return (
        <AuthContext.Provider value={{user, updateUser}}>
            <div className="App">
                <RouterProvider router={router}/>
            </div>
        </AuthContext.Provider>
    )
}

export default App
