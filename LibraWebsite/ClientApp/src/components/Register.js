import { extend } from 'jquery';
import React, { Component, useState } from 'react';
import ReactDOM from 'react-dom';
import './LoginStyle.css';
import userSVG from './img/undraw_male_avatar_323b.svg';
import logo from './img/libra500.png';
import { useCookies, Cookies } from 'react-cookie';

export class Register extends Component {
    static displayName = Register.name;
    constructor() {
        super();
        this.state = { isError: false }
        this.handleRegister = this.handleRegister.bind(this)
    };

    async handleRegister() {
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                id: 0,
                username: new String(document.getElementById('username').value),
                email: new String(document.getElementById('email').value),
                password: new String(document.getElementById('password').value),
                recoveryPassword: null,
                recoveryDate: new Date(2006, 0, 2, 15, 4, 5)
            })

        };
        const response = await fetch('https://cors-anywhere.herokuapp.com/' + 'adress' + 'api/User', requestOptions);
        const data = await response.json().then(data => { console.log(data) });
    }

    render() {
        return (
            <body>

                <div className="myContainer">

                    <div className="img">
                        <img src={logo} />
                    </div>

                    <div class="login-content">
                        <div className="form">
                            <img src={userSVG} />
                            <h2 className="title">Sign up</h2>

                            <div className="input-div one">
                            <div className="i">
                                <i className="fas fa-user"></i>
                            </div>
                                <div className="div">
                                    <h5>Username</h5>
                                    <input name="username" type="text" className="input" id="username" />
                                </div>
                            </div>

                            <div className="input-div one">
                                <div className="i">
                                    <i className="fas fa-user"></i>
                                </div>
                                <div className="div">
                                    <h5>Email</h5>
                                    <input name="email" type="text" className="input" id="email" />
                                </div>
                            </div>
                            <div className="input-div pass">
                                <div className="i">
                                    <i className="fas fa-lock"></i>
                                </div>
                                <div className="div">
                                    <h5>Password</h5>
                                    <input name="password" type="password" className="input" id="password" />
                                </div>
                            </div>
                            <a href="Login">Already have an account?</a>
                            <input type="submit" className="btn" value="Register" onClick={this.handleRegister} />
                        </div>
                    </div>
                </div>

            </body>
        );
    }
}
