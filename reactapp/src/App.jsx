import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import Navbar from './components/Navbar'
import Home from './components/Home'
import { BrowserRouter, Routes, Route } from 'react-router-dom'

function App() {

    return (
        <div className="App">
            <BrowserRouter>
                <Navbar className="navbar" />
                <Routes>
                    <Route index element={<Home />} />
                </Routes>
            </BrowserRouter>
        </div>

    )
}

export default App
