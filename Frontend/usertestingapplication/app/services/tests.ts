export const getAllTests = async () => {
	const accessToken = localStorage.getItem('accessToken')
	if (!accessToken) {
		throw new Error('Access token is missing')
	}

	const response = await fetch('https://localhost:7212/api/tests', {
		headers: {
			Authorization: `Bearer ${accessToken}`,
		},
	})
	return response.json()
}

export const getAvailableTests = async () => {
	const accessToken = localStorage.getItem('accessToken')
	if (!accessToken) {
		throw new Error('Access token is missing')
	}

	const response = await fetch('https://localhost:7212/available', {
		headers: {
			Authorization: `Bearer ${accessToken}`,
		},
	})
	return response.json()
}

export const getCompletedTests = async () => {
	const accessToken = localStorage.getItem('accessToken')
	if (!accessToken) {
		throw new Error('Access token is missing')
	}

	const response = await fetch('https://localhost:7212/completed', {
		headers: {
			Authorization: `Bearer ${accessToken}`,
		},
	})
	return response.json()
}
