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
                <div class="checkbox">
                    <input type="checkbox" id="BrandAsus" name="Asus" />
                    <label for="BrandAsus"> Asus</label>
                    <br />
                    <input type="checkbox" id="BrandApple" name="Apple" />
                    <label for="BrandAplee"> Apple</label>
                    <br />
                    <input type="checkbox" id="BrandLenovo" name="Levono" />
                    <label for="BrandLenovo"> Lenovo</label>
                    <br />
                    <input type="checkbox" id="BrandAcer" name="Acer" />
                    <label for="BrandAcer"> Acer</label>
                    <br />
                    <input type="checkbox" id="BrandDell" name="Dell" />
                    <label for="BrandDell"> Dell</label>
                    <br />
                    <input type="checkbox" id="BrandHuawei" name="Huawei" />
                    <label for="BrandHuawei"> Huawei</label>
                </div>
            </div>
        );

    }
}
