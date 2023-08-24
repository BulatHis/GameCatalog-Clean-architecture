import * as React from 'react';
import {useEffect, useState} from 'react';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import {createTheme, ThemeProvider} from '@mui/material/styles';
import {useLocation, useNavigate, useParams} from "react-router-dom";

export default function WriteReview() {
    const {Id} = useParams();
    const location = useLocation();
    const gameName = new URLSearchParams(location.search).get('game');
    const gameId = new URLSearchParams(location.search).get('gameId');
    const [text, setText] = useState('');
    const [rating, setRating] = useState('');
    const [errorText, setErrorText] = useState('');
    const [errorRating, setErrorRating] = useState('');
    const [errorSend, setErrorSend] = useState('');
    const navigate = useNavigate();

    const validatePassword = (text) => {
        if (text.length < 1 || text.text > 500) {
            setErrorText('text must be between 1 and 500 characters');
        } else {
            setErrorText('');
        }
    };
    const validateRating = (text) => {
        if (isNaN(text)) {
            setErrorRating('Rating must be a number');
        } else if (text === 0 || text > 5 || text < 1) {
            setErrorRating('Rating must be between 1 and 5');
        } else {
            setErrorRating('');
        }
    }


    const handleSubmit = (event) => {
        event.preventDefault();
        console.log('Text:', text);
        console.log('Rating:', rating);
    };

    const loginOnClick = () => {
        if (text === "" || rating === "") {
            setErrorSend('text & rating is EMPTY');
        } else {
            fetch('http://localhost:4044/addreview', {
                method: 'POST', mode: 'cors', headers: {
                    'Content-Type': 'application/json', 'Authorization': "Bearer " + localStorage.getItem('accessToken')
                }, body: JSON.stringify({
                    text: text, rating: rating, GameId: gameId
                })
            }).then(response => {
                if (response.ok) {
                    navigate(`/game/${gameName}`);
                    return response.json();
                } else {
                    throw new Error('Ошибка при отправке данных на сервер');
                }
            })
                .catch(error => {
                    refreshRefreshToken();
                    console.error('Ошибка при отправке данных на сервер', error);
                });
        }
    }
    const refreshRefreshToken = () => {
        fetch('http://localhost:4044/refreshtoken', {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                refresh: localStorage.getItem('refreshToken')
            })
        }).then(response => response.json())
            .then(data => {
                localStorage.setItem('refreshToken', data.refreshToken);
                localStorage.setItem('accessToken', data.accessToken);
            })
            .catch(error => {
                console.error('Ошибка при отправке запроса', error);
            });
    }


    return (<div>
        <h1>Write Your Review on {gameName}</h1>
        <form onSubmit={handleSubmit}>
            <Grid item xs={10}>
                <TextField
                    onChange={(p) => {
                        setText(p.target.value);
                    }}
                    onBlur={(p) => {
                        validatePassword(p.target.value);
                    }}

                    required
                    fullWidth
                    name="text"
                    label="Your Review"
                    type="text"
                    id="text"
                    autoComplete="text"
                    error={Boolean(errorText)}

                    helperText={errorText}
                />
            </Grid>
            <Grid item xs={10}>
                <TextField
                    onChange={(p) => {
                        setRating(p.target.value);
                    }}
                    onBlur={(p) => {
                        validateRating(p.target.value);
                    }}
                    required
                    fullWidth
                    name="rating"
                    label="Your rating"
                    type="rating"
                    id="rating"
                    autoComplete="rating"
                    error={Boolean(errorRating)}
                    helperText={errorRating}
                />
            </Grid>
            <Button onClick={loginOnClick}
                    fullWidth
                    variant="contained"
                    sx={{mt: 3, mb: 2}}
            >
                Add
            </Button>
            {errorSend && <p>{errorSend}</p>}
        </form>
    </div>);
}