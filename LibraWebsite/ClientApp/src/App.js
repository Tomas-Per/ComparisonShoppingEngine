import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Route, Router } from 'react-router';
import { Layout } from './components/Layout';
import { Categories } from './components/Categories';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { ProductsMenu } from './components/ProductsMenu';
import { Rating } from './components/Rating';
import { Login } from './components/Login'
import { Register } from './components/Register'
import './custom.css'
import { ComparisonTab } from './components/ComparisonTab';

export default class App extends Component {
    static displayName = App.name;

render() {
    return (
        <Layout>
            <Route exact path='/' component={Categories} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
            <Route path='/categories' component={Categories} />
            <Route path='/products/:category/:page' component={ProductsMenu} />
            <Route path='/Login' component={Login} />
            <Route path='/Register' component={Register} />
            <Route path='/comparison' component={ComparisonTab} />

        <Route path='/ratings/:category/:id' component={Rating} />
        
      </Layout>
    );
  }
}

