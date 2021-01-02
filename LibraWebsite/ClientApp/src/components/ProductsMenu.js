import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import GridList from './GridList';


export class ProductsMenu extends Component {
    static displayName = ProductsMenu.name;

    constructor(props) {
        super(props);
        this.state = { checked: false };
    }

    render() {
        const { category, page } = this.props.match.params;
        console.log(category);
        console.log(page);
        return (
            <div>
                <GridList category={category} page={page} style={{marginLeft: 'auto', marginRight: 'auto', width: '100%'}}/>
                
            </div>
        );

    }
}