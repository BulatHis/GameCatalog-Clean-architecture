import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";

export default function Game() {
    const {title} = useParams();
    const [gameData, setGameData] = useState(null);

    useEffect(() => {
        const fetchGameInfo = async () => {
            try {
                const response = await fetch(`https://api.rawg.io/api/games?search=${title}`);
                const data = await response.json();
                if (data.results && data.results.length > 0) {
                    const game = data.results[0]; // Получаем информацию только о первой найденной игре
                    setGameData(game);
                } else {
                    throw new Error('Игра не найдена');
                }
            } catch (error) {
                console.error('Ошибка при получении информации об игре', error);
            }
        };

        fetchGameInfo();
    }, [title]);

    if (!gameData) {
        return <p>Loading...</p>;
    }

    const {developer, publisher, genre, name: englishName, description} = gameData;

    return (
        <div>
            <h1>{englishName}</h1>
            <p>Publisher: {publisher}</p>
            <p>Developer: {developer}</p>
            <p>Genre: {genre}</p>
            <p>Description: {description}</p>
        </div>
    );
};