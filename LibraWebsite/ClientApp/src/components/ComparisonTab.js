import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { instanceOf } from 'prop-types';
import { withCookies, Cookies, useCookies } from 'react-cookie';
import './ComparisonTab.css';
import ComparisonItem from './ComparisonItem';

export class ComparisonTab extends Component {
    static displayName = ComparisonTab.name;

    render() {
      return (
        <div>
            <div className={"gridComparison"}>
                  <div className={"column1"}>
                      <ComparisonItem item={1} colorColumn={"rgb(229, 161, 219)"}/>
   
                </div>


                 <div>
                      <svg viewBox="0 0 400 250" />
                      <script src='https://cdnjs.cloudflare.com/ajax/libs/d3/5.9.2/d3.min.js' />
                      <script src='./Chart.js'/>
                </div>


                  <div className={"column2"}>
                      <ComparisonItem item={2} colorColumn={"rgb(128, 222, 234)"}/>
                </div>
            </div>
        </div>
      );
     
  }
}
