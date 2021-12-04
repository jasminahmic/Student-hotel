import React from 'react'
import NavigationItem from '../NavigationItems/NavigationItem/NavigationItem'
// import NavigationItems from '../NavigationItems/NavigationItems'
import './Toolbar.css'
var Toolbar = () => {
    return (
        <div className='Toolbar'>
            <nav>
                <ul>
                {/* <NavigationItems /> */}
                <NavigationItem>Log Out</NavigationItem>
                </ul>
            </nav>
        </div>)
}
export default Toolbar;