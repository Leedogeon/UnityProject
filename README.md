# UnityProject

 인게임 플레이 화면
 
<img width="640" height="360" alt="RopeActionTest_GitHub" src="https://github.com/user-attachments/assets/9522ad4b-d5a8-48ac-a501-d8c255e9a4ea" />

>[!Note]
> 로프액션 코드<br>
> 카메라에서 목표물로 SpringJoint를 이용하여 로프 생성

    void RopeShoot()
    {
        if(Physics.Raycast(FollowCamera.transform.position, FollowCamera.transform.forward, out hit, Length, HitLayer))
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            IsGrappling = true;
            Lr.positionCount = 2;
            Lr.SetPosition(1, hit.point);
            Sj = Player.gameObject.AddComponent<SpringJoint>();
            //앵커의 위치 자동설정 false
            Sj.autoConfigureConnectedAnchor = false;
            Sj.connectedAnchor = hit.point;

            Sj.maxDistance = distance;
            Sj.minDistance = distance * .5f;
            Sj.spring = 2f; //강도
            Sj.damper = 3f; //줄어드는 힘
            Sj.massScale = 1f;
        }

    }

- 처음엔 플레이어에게서 목표물을 향해 로프를 발사했는데, 이는 추가적인 보정이 없으면 플레이어의 마우스 위치와는 다른곳으로 로프가 향했다
- 이를 해결하기위해 단순하게 접근하여 카메라에서 Raycast를 이용하여 목표지점과 연결하게 변경하여서 작동하게 바꾸었더니 해결되었다 




사용자의 PC에 점수를 저장하고 기록

<img width="640" height="360" alt="RopeActionScore_GitHub" src="https://github.com/user-attachments/assets/0b8769b7-742a-4594-8183-0ae7561ebbda" />

>[!Note]
> 유니티에서 기본적으로 설정해진 위치에 폴더를 생성하고 파일을 저장

public static void SavePlayer(PlayerAction action, PlayerInfo info)
{
    BinaryFormatter formatter = new BinaryFormatter();

    // 프로젝트 경로에서 Save 폴더에 저장
    string projectPath = Directory.GetParent(Application.dataPath).FullName; // 프로젝트 경로
    string savePath = Path.Combine(projectPath, "Save", "player.save"); // Save 폴더 경로

    // Save 폴더가 존재하지 않으면 생성
    if (!Directory.Exists(Path.GetDirectoryName(savePath)))
    {
        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
    }
    .
    .
    .
}

>[!Note]
> 저장된 파일 위치에서 세이브 파일에 대한 정보를 가져오기

public static PlayerDataSave LoadPlayer(PlayerAction action, PlayerInfo info)
{
    string projectPath = Directory.GetParent(Application.dataPath).FullName; // 프로젝트 경로
    string loadPath = Path.Combine(projectPath, "Save", "player.save"); // Save 폴더 경로

    if (File.Exists(loadPath))
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(loadPath, FileMode.Open))
        {
            PlayerDataSave data = formatter.Deserialize(stream) as PlayerDataSave;
            return data;
        }
    }
    else
    {
        Debug.Log("Save file not found in " + loadPath);
        return null;
    }
}

