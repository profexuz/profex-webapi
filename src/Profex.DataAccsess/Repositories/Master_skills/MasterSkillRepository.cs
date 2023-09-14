using Dapper;
using Profex.Application.Exceptions.Masters;
using Profex.Application.Exceptions.Skills;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Master_skills;
using Profex.Domain.Entities.master_skills;

namespace Profex.DataAccsess.Repositories.Master_skills
{
    public class MasterSkillRepository : BaseRepository, IMasterSkillRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from master_skills";
                var result = await _connection.QuerySingleAsync<long>(query);

                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> CreateAsync(Master_skill entity)
        {
            try
            {
                await _connection.OpenAsync();

                string checkMasterQuery = "SELECT COUNT(*) FROM masters WHERE id = @MasterId";
                string checkSkillQuery = "SELECT COUNT(*) FROM skills WHERE id = @SkillId";

                var masterExists = await _connection.ExecuteScalarAsync<int>(checkMasterQuery, new { MasterId = entity.MasterId });
                var skillExists = await _connection.ExecuteScalarAsync<int>(checkSkillQuery, new { SkillId = entity.SkillId });

                if (masterExists == 0)
                {
                    throw new MasterNotFoundException();
                }
                if (skillExists == 0)
                {
                    throw new SkillNotFoundException();
                }




                string query = "INSERT INTO public.master_skills(master_id, skill_id, created_at, updated_at)" +
                    "VALUES (@MasterId, @SkillId, @CreatedAt, @UpdatedAt);";

                var result = await _connection.ExecuteAsync(query, entity);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "DELETE FROM master_skills WHERE id=@Id";
                var result = await _connection.ExecuteAsync(query, new { Id = id });

                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IList<Master_skill>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM master_skills order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

                var result = (await _connection.QueryAsync<Master_skill>(query)).ToList();

                return result;
            }
            catch
            {
                return new List<Master_skill
                    >();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Master_skill?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM master_skills where id=@Id";
                var result = await _connection.QuerySingleAsync<Master_skill>(query, new { Id = id });

                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> UpdateAsync(long id, Master_skill entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"UPDATE public.master_skills " +
                    $"SET master_id=@MasterId, skill_id=@SkillId, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                        $"WHERE id={id}";

                var result = await _connection.ExecuteAsync(query, entity);

                return result;
            }
            catch //(Exception ex)
            {
                //throw new Exception(ex.Message);
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IList<Master_skill>> GetMasterAllSkillAsync(long masterId)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM public.master_skills WHERE master_id = {masterId}";

                return (await _connection.QueryAsync<Master_skill>(query)).ToList();
            }
            catch
            {
                return new List<Master_skill>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Master_skill> GetMasterSkillIdAsync(long masterId, long skillId)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM master_skills where master_id = {masterId} and skill_id = {skillId}";
                var result = await _connection.QuerySingleAsync<Master_skill>(query);

                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> DeleteMasterSkillIdAsync(long masterId, long skillId)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"DELETE FROM master_skills WHERE master_id = {masterId} and skill_id = {skillId}";
                var result = await _connection.ExecuteAsync(query);

                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }
    }
}
