import React, { Component } from 'react';
import './RatingStyle.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faStar } from '@fortawesome/free-solid-svg-icons';


export class Rating extends Component {
    static displayName = Rating.name;
    constructor(props) {
        super(props);
        this.state = { checked: false };
    }   

    render() {
        const { category, id } = this.props.match.params;
        return (
            <div>
                <div>
                  
                </div>
            <div className="starContainer">
                <div class="star-widget">
                    <input type="radio" name="rate" id="rate-5" />
                    <label htmlFor="rate-5"> <FontAwesomeIcon icon={faStar} /></label>
                    <input type="radio" name="rate" id="rate-4" />
                    <label htmlFor="rate-4"> <FontAwesomeIcon icon={faStar} /></label>
                    <input type="radio" name="rate" id="rate-3" />
                    <label htmlFor="rate-3"> <FontAwesomeIcon icon={faStar} /></label>
                    <input type="radio" name="rate" id="rate-2" />
                    <label htmlFor="rate-2"> <FontAwesomeIcon icon={faStar} /></label>
                    <input type="radio" name="rate" id="rate-1" />
                    <label htmlFor="rate-1"> <FontAwesomeIcon icon={faStar} /></label>
                    <form>
                        <header></header>
                        <center><div className="textarea">
                            <textarea cols="30"> </textarea>
                        </div> </center>
                        <div className="btn">
                            <button type="submit"> Post </button>
                        </div>
                    </form>                   
                </div>
                </div>
            </div>


        );
    }
}

