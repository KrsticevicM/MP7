import './Navbar.css'

function Navbar() {
    return (
        <div className='navbar-container'>
            <img alt="logo" src="/images/Dog_Paw_Print.png" className='logo-img' />
            <div className='navbar-inner-container'>
                <a className='navbar-anchor'>
                    <div className='left-container'>POČETNA</div>
                </a>
                <a className='navbar-anchor'>
                    <div className='middle-container'>SKLONIŠTA</div>
                </a>
                <a className='navbar-anchor'>
                    <div className='right-container'>PRIJAVI SE</div>
                </a>
            </div>
        </div>
    )
}

export default Navbar