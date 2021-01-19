import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { instanceOf } from 'prop-types';
import { withCookies, Cookies, useCookies } from 'react-cookie';
import './ComparisonTab.css';
import ComparisonItem from './ComparisonItem';
import ComparisonCharts from './ComparisonCharts';

export class ComparisonTab extends Component {
    static displayName = ComparisonTab.name;

    render() {
      return (
        <div>
            <div className={"gridComparison"}>
                  <div className={"column1"}>
                      <ComparisonItem item={1} colorColumn={"rgb(229, 161, 219)"}/>
   
                </div>


                  <div style={{width: '100%',transform: 'translateX(10%)'}}>
                      <ComparisonCharts/>
                </div>


                  <div className={"column2"}>
                      <ComparisonItem item={2} colorColumninfo={"rgb(128, 222, 234)"}/>
                </div>
            </div>
        </div>
      );
     
  }
}
