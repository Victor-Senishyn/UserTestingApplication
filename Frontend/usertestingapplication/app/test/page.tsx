'use client'
import React, { useEffect, useState } from 'react'

const Test = () => {
	const [id, setId] = useState<string | null>(null)

	useEffect(() => {
		const urlParams = new URLSearchParams(window.location.search)
		const idParam = urlParams.get('id')
		if (idParam) {
			setId(idParam)
		}
	}, [])
	//getQuestions(id)
	return <div className='card__buttons'>{id}</div>
}

export default Test
