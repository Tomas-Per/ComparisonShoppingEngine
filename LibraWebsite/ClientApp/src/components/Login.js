import { extend } from 'jquery';
import React, { Component, useState } from 'react';
import ReactDOM from 'react-dom';
import './LoginStyle.css';
import userSVG from './img/undraw_male_avatar_323b.svg';
import logo from './img/libra500.png';

export class Login extends Component {
    static displayName = Login.name;

    handleLogin() {
    var proxyUrl = 'https://cors-anywhere.herokuapp.com/',
            targetUrl = '' + 'Login/' + document.getElementById('email').value + '/' + document.getElementById('password').value 
    fetch(proxyUrl + targetUrl)
        .then((response) => {
            console.log(document.getElementById('email').value);
            console.log(document.getElementById('password').value);
            console.log(targetUrl);
            console.log('Accessing...');
            if (!response.ok) throw new Error(response.status);
            else { console.log(response); return response.json(); }
        })
        .catch((error) => {
            console.log(error);
        });
}

    render() {
        return (
            <body>
                 
	            <div className="myContainer">

                    <div className="img">
                            <img src={logo}/>
                    </div>

                    <div class="login-content">
                        <form>
                                <img src={userSVG} />
				            <h2 className="title">Welcome</h2>
           		            <div className="input-div one">
           		   <div className="i">
           		   		<i className="fas fa-user"></i>
           		   </div>
           		   <div className="div">
                                    <h5>Username</h5>
                                    <input name="email" type="text" className="input" id="email"/>
           		   </div>
           		</div>
           		<div className="input-div pass">
           		   <div className="i"> 
           		    	<i className="fas fa-lock"></i>
           		   </div>
           		   <div className="div">
                                    <h5>Password</h5>
                                    <input name="password" type="password" className="input" id="password"/>
            	   </div>
            	</div>
                            <a href="#">Forgot Password?</a>
                            <input type="submit" className="btn" value="Login" onClick={ this.handleLogin } />
            </form>
        </div>
                </div>
               
            </body>
            );
    }
}

