import React, { Component } from 'react';
import './FilterStyle.css';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import Slider from './Slider'
import CheckBox from './Checkbox'
import { Grid } from '@material-ui/core';
import Checkbox from '@material-ui/core/Checkbox';
import logo from './img/libra500.png';



export default class Filter extends Component {
    render() {
        return (
            <navbar id="mySidenav" className="sidenav">
                    
            </navbar>
        );

    }
}
/*  return (
            <navbar id="mySidenav" className="sidenav">
                <Grid className="myGrid">
                    <h5>Filters</h5>
                </Grid>

                <p>Price up to:</p>
                <ul id="slider" className="slider">
                    <Slider />
                </ul>
                <p>Processor</p>
                <div className="checkbox">
                    <CheckBox id="ProcessorInteli3" name="Inteli3" label="Intel i3" />
                    <CheckBox id="ProcessorInteli5" name="Inteli5" label="Intel i5" />
                    <CheckBox id="ProcessorInteli7" name="Inteli7" label="Intel i7" />
                    <CheckBox id="ProcessorAMD3" name="AMD3" label="AMD Ryzen 3" />
                    <CheckBox id="ProcessorAMD5" name="AMD5" label="AMD Ryzen 5" />
                    <CheckBox id="ProcessorAMD7" name="AMD7" label="AMD Ryzen 7" />
                </div>
                <p>Brand</p>
                <div className="checkbox">
                    <CheckBox id="BrandAsus" name="Asus" label="Asus" />
                    <CheckBox id="BrandDell" name="Dell" label="Dell" />
                    <CheckBox id="BrandApple" name="Apple" label="Apple" />
                    <CheckBox id="BrandAcer" name="Acer" label="Acer" />
                    <CheckBox id="BrandLenovo" name="Lenovo" label="Lenovo" />
                    <CheckBox id="BrandHuawei" name="Huawei" label="Huawei" />
                </div>
            </navbar>
        );*/
