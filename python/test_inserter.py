import requests
api_url = 'http://localhost:8080/api'


# json is a dict object
def post_request(endpoint, json):
    headers = {
        "Content-Type": "application/json"
    }
    return requests.post(api_url + endpoint, json=json, headers=headers)


if __name__ == '__main__':
    json = {"sceneId": 4, "inProgress": True, "participants": '1, 5', "winningVote": None, "flavor": ''}
    post_request('/performance', json)

    json = {"sceneId": 4, "inProgress": True, "participants": '1, 5', "winningVote": None, "flavor": ''}
    post_request('/performance', json)

    json = {"sceneId": 4, "inProgress": True, "participants": '1, 5', "winningVote": None, "flavor": ''}
    post_request('/performance', json)

    json = {"sceneId": 4, "inProgress": True, "participants": '1, 5', "winningVote": None, "flavor": ''}
    post_request('/performance', json)
