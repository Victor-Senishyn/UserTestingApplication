export const getQuestions = async (id:number) => {
	const accessToken = localStorage.getItem('accessToken')
	if (!accessToken) {
		throw new Error('Access token is missing')
	}

	const response = await fetch(
		`https://localhost:7212/api/tests/${id}/questions`,
		{
			headers: {
				Authorization: `Bearer ${accessToken}`,
			},
		}
	)
	return response.json()
}
