import axios from 'axios'

export const setAuthToken = (token: string | null) => {
	if (token) {
		axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
	} else {
		delete axios.defaults.headers.common['Authorization']
	}
}

export const signUp = async (email: string, password: string) => {
	const response = await axios.post('https://localhost:7212/register', {
		email,
		password,
	})
	await signIn(email, password)
	return response.data
}

export const signIn = async (email: string, password: string) => {
	const response = await axios.post('https://localhost:7212/login', {
		email,
		password,
	})
	if (!response.data || !response.data.accessToken) {
		throw new Error('Invalid response data')
	}
	const { accessToken } = response.data
	localStorage.setItem('accessToken', accessToken)
	setAuthToken(accessToken)
	return response.data
}
