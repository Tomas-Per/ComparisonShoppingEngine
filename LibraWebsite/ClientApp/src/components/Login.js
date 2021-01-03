import { extend } from 'jquery';
import React, { Component, useState } from 'react';
import ReactDOM from 'react-dom';
import './LoginStyle.css';
import userSVG from './img/undraw_male_avatar_323b.svg';
import logo from './img/libra500.png';

export class Login extends Component {
    static displayName = Login.name;

    render() {
        return (
            <body>
                 
	            <div className="container">

                    <div className="img">
                            <img src={logo}/>
                    </div>

                    <div class="login-content">
                        <form action="index.html">
                                <img src={userSVG} />
				            <h2 className="title">Welcome</h2>
           		            <div className="input-div one">
           		   <div className="i">
           		   		<i className="fas fa-user"></i>
           		   </div>
           		   <div className="div">
                                    <h5>Username</h5>
                                    <input type="text" className="input"/>
           		   </div>
           		</div>
           		<div className="input-div pass">
           		   <div className="i"> 
           		    	<i className="fas fa-lock"></i>
           		   </div>
           		   <div className="div">
                                    <h5>Password</h5>
                                    <input type="password" className="input" />
            	   </div>
            	</div>
                            <a href="#">Forgot Password?</a>
                            <input type="submit" className="btn" value="Login" />
            </form>
        </div>
                </div>
                <script type="text/javascript" src="index.html"></script>
            </body>
            );
    }
}
