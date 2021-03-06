﻿import { extend } from 'jquery';
import React, { Component, useState } from 'react';
import ReactDOM from 'react-dom';

import userSVG from './img/undraw_male_avatar_323b.svg';
import logo from './img/libra500.png';
import { useCookies, Cookies } from 'react-cookie';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export class Login extends Component {
    static displayName = Login.name;
    constructor() {
        super();
        this.state = { isError: false }
        this.handleLogin = this.handleLogin.bind(this)
    };

    handleLogin() {
        const cookies = new Cookies();
        var proxyUrl = 'https://cors-anywhere.herokuapp.com/',
            targetUrl = process.env.REACT_APP_API + 'Login/' + document.getElementById('email').value + '/' + document.getElementById('password').value
        fetch(targetUrl)
            .then((response) => {
                console.log(document.getElementById('email').value);
                console.log(document.getElementById('password').value);
                console.log(targetUrl);
                console.log('Accessing...');
                if (!response.ok) {
                    toast("Invalid login credentials", {
                        className: "pink-toast",
                        draggable: true,
                        position: toast.POSITION.BOTTOM_RIGHT,
                        progressClassName: "pink-toast"
                    });
                    throw new Error(response.status);
                }
                else { console.log('Success'); response.json().then(data => { cookies.set('user', JSON.stringify(data), { path: '/' }); }); window.location.href = "/"; }
            })
            .catch((error) => {
                console.log(error);
                this.setState({isError:true});
            });
    }


    render() {
        return (
            <body>
                <ToastContainer />
	            <div className="myContainer">
                    <div className="img">
                        <img src={logo} />
                    </div>

                    <div class="login-content">
                        <div className="form">
                                <img src={userSVG} />
				            <h2 className="title">Welcome</h2>
           		            <div className="input-div one">
           		                <div className="i">
           		   		            <i className="fas fa-user"></i>
           		                </div>
           		                <div className="div">
                                    <input name="email" type="text" className="input" id="email" placeholder="Email"/>
           		                </div>
           		            </div>
           		            <div className="input-div pass">
           		               <div className="i"> 
           		    	            <i className="fas fa-lock"></i>
           		               </div>
           		               <div className="div">
                                                <input name="password" type="password" className="input" id="password" placeholder="Password"/>
            	               </div>
            	            </div>
                            <a href="Register">Register</a>
                            <a href="#">Forgot Password?</a>
                            <input type="submit" className="btn" value="Login" onClick={this.handleLogin} /> 
                        </div>
                    </div>
                </div>
                
            </body>
            );
    }
}



