import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import GridList from './SimilarGridList';
import SelectedItem from './ComparisonItem';


export class SimilarList extends Component {
    static displayName = SimilarList.name;

    constructor(props) {
        super(props);
        this.state = { checked: false };
    }

    render() {
        const { category} = this.props.match.params;
        return (
            <div>
                <div style={{dislay: 'flex', maxWidth: '700px', marginLeft: 'auto', marginRight: 'auto'}}>
                    <SelectedItem item={3} colorColumn={"rgb(229, 161, 219)"} />
                    <br />
                    <center> Similar Products: </center>
                    <br />
                    </div>
                <GridList category={category} style={{marginLeft: 'auto', marginRight: 'auto', width: '100%'}}/>
                
            </div>
        );

    }
}