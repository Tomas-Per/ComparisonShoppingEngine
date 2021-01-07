import React, { Component } from 'react';
import './CategoryMenu.css';

export class Categories extends Component {
  static displayName = Categories.name;

  render () {
    return (
        <div className={"gridCategories"}>
            <div className={"categoryCart"}>
                <img src="https://i.ibb.co/kGz0B3W/Decktop-cart.png" alt="Decktop-cart" border="0" />      
                <a href="products/Desktops/1">
                    <img src="https://i.ibb.co/wwJWvKC/button-png.png" alt="button-png" border="0" className={"exploreButton"} />
                </a>
            </div>
            <div className={"categoryCart"}>
                <img src="https://i.ibb.co/hg0Vd6N/Laptop-cart.png" alt="Laptop-cart" border="0" />      
                <a href="products/Laptops/1">
                    <img src="https://i.ibb.co/wwJWvKC/button-png.png" alt="button-png" border="0" className={"exploreButton"} />
                </a>
            </div>
            <div className={"categoryCart"}>
                <img src="https://i.ibb.co/nMXKT02/smartphone-cart.png" alt="smartphone-cart" border="0" />      
                <a href="products/Smartphones/1">
                    <img src="https://i.ibb.co/wwJWvKC/button-png.png" alt="button-png" border="0" className={"exploreButton"} />
                </a>
             </div>
        </div>
      );
     
  }
}
/*<a href="https://imgbb.com/"><img src="https://i.ibb.co/wwJWvKC/button-png.png" alt="button-png" border="0"></a>
    <a href="https://imgbb.com/"><img src="https://i.ibb.co/kGz0B3W/Decktop-cart.png" alt="Decktop-cart" border="0"></a>
        <a href="https://imgbb.com/"><img src="https://i.ibb.co/hg0Vd6N/Laptop-cart.png" alt="Laptop-cart" border="0"></a>
            <a href="https://imgbb.com/"><img src="https://i.ibb.co/nMXKT02/smartphone-cart.png" alt="smartphone-cart" border="0"></a>*/