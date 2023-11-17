import React from 'react'
import Navbar from '../components/Navbar'
import { Outlet } from 'react-router-dom'

export const RootLayout = () => {
  return (
    <div className='root-layout'>
        <Navbar/>

        <Outlet/>

    </div>
  )
}
