import React from 'react'
import Sidebar from '../../Component/Sidebar/Sidebar';
import './Layout.css'
var Layout=(props)=>{
    return(
        <div>
           {/* <Toolbar/> */}
           <Sidebar/>

            <main>
                {props.children}
            </main>
            <footer>
                ovo je footer
            </footer>
        </div>
    )
}
export default Layout;