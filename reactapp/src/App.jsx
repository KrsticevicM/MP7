import './App.css'
import Home from './components/Home'
import {Route, createBrowserRouter, createRoutesFromElements, RouterProvider } from 'react-router-dom'
import Login from './components/Login'
import Registration from './components/Registration'
import { RootLayout } from './layouts/RootLayout'
import { NotFound } from './components/NotFound'
import Ad_detail from './components/Ad_detail'
import Shelter from './components/Shelter'
import { AuthContext } from './components/AuthenticationContext'
import { useState } from 'react'
import MyAds from './components/MyAds'


const router=createBrowserRouter(
    createRoutesFromElements(
        
        <Route path="/" element={<RootLayout/>}>

            <Route index element={<Home />} />

            <Route path='login' element={<Login />}/>

            <Route path=":id" element={<Ad_detail />}></Route>

            <Route path="/sklonista" element={<Shelter />} />

            <Route path="/moji-oglasi" element={<MyAds />} />

            <Route path='registration' element={<Registration/>}/>

            <Route path="*" element={<NotFound/>}/> {/*error page */}
            
            
        </Route>

        
    )
)



function App() {
    
    const [user, setUser] = useState({
        isAuth: false,
        userID: null,
        firstName: '',
        lastName: '',
    })
    
    
    const updateUser = (newUserData) => {
        setUser((prevUser) => ({ ...prevUser, ...newUserData }));
    }
    

    return (
        <AuthContext.Provider value={{user, updateUser}}>
            <div className="App">
                <RouterProvider router={router}/>
            </div>
        </AuthContext.Provider>
    )
}

export default App
