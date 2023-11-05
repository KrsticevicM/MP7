import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import Navbar from './components/Navbar'
import Home from './components/Home'

function App() {

    return (
        <div className="App">
            <Navbar className="navbar" />
            <Home className="home" />
        </div>
    )
}

export default App
