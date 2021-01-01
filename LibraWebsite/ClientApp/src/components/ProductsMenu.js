import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import GridList from './GridList';

export class ProductsMenu extends Component {
    static displayName = ProductsMenu.name;

    render() {
        const { category, page } = this.props.match.params;
        console.log(category);
        console.log(page);
        return (
            <GridList category={category} page={page}/>
        );

    }
}