import React, { Component } from 'react';
import './FilterStyle.css';
import Slider from './Slider'
import { Grid } from '@material-ui/core';



export default class Filter extends Component {
    render() {
        return (
            <div id="mySidenav" class="sidenav">
                <Grid>
                    <h5>Filters</h5>
                </Grid>
                
                <p>Price up to:</p>
                <ul id="slider">
                    <Slider />
                </ul>
                <p>Processor</p>
                <p>Brand</p>
                <input type="checkbox" id="BrandAsus" name="Asus" value="Asus"/>
                    <label for="BrandAsus"> Asus</label>
            </div>
        );

    }
}
