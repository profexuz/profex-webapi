using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Masters1;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.DataAccsess.ViewModels.Skills;
using Profex.Domain.Entities.master_skills;
using Profex.Domain.Entities.masters;
using Profex.Domain.Entities.skills;
using static Dapper.SqlMapper;

namespace Profex.DataAccsess.Repositories.Masters1
{
    public class Master1Repository : BaseRepository, IMaster1Repository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from masters";
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

        public Task<int> CreateAsync(Master entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "DELETE FROM masters WHERE id=@Id";
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

        public async Task<IList<MasterViewModel>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM public.masters ORDER BY id desc offset {@params.GetSkipCount()} " +
                    $"limit {@params.PageSize}";

                var resMas = (await _connection.QueryAsync<Master>(query)).ToList();
                
                var masterViewModels = resMas.Select(master => new MasterViewModel
                {
                    Id = master.Id,
                    FirstName = master.FirstName,
                    LastName = master.LastName,
                    ImagePath = master.ImagePath,
                    IsFree = master.IsFree,
                    PhoneNumber = master.PhoneNumber,
                    CreatedAt=master.CreatedAt,
                    UpdatedAt=master.UpdatedAt,
                }).ToList();

                return masterViewModels;
            }
            catch
            {
                return new List<MasterViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<MasterViewModel?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string qeury = $"SELECT * FROM masters where id=@Id";
                var result = await _connection.QuerySingleAsync<MasterViewModel>(qeury, new { Id = id });

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

        public async Task<Master?> GetByPhoneAsync(string phone)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "SELECT * FROM public.masters WHERE phone_number=@PhoneNumber;";
                var result = await _connection.QueryFirstOrDefaultAsync<Master>(query, new { PhoneNumber = phone });

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

        public async Task<IList<UserSkillViewModel>> GetMasterSkillById(long masterId)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $@"
            SELECT
                m.first_name,
                m.last_name,
                m.phone_number,
                m.image_path,
                m.is_free,
                m.created_at,
                m.updated_at,
                s.title AS skill_title FROM masters AS m JOIN skills AS s ON m.masterId = s.masterId;";

                var result = await _connection.QueryAsync<UserSkillViewModel>(query, new { MasterId = masterId });
                return result.ToList();
            }



            catch
            {
                //.//return MasterViewModel();
                return new List<UserSkillViewModel>();
            }
            finally
            {
                await _connection.CloseAsync(); 
            }
        }

        public async Task<IList<MasterViewModel>> SearchAsync(string search, PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM public.masters WHERE first_name ILIKE '%{search}%' " +
                    $"ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

                var master = await _connection.QueryAsync<MasterViewModel>(query);

                return master.ToList();
            }
            catch
            {
                return new List<MasterViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> SearchCountAsync(string search)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT COUNT(*) FROM public.masters WHERE name ILIKE '%{search}%'";
                var count = await _connection.ExecuteScalarAsync<int>(query);

                return count;
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

        public async Task<IList<Master_skill>> SortBySkillId(long skillId)
        {
            string query = "SELECT * FROM master_skills WHERE skill_id = @SkillId";
            var parameters = new { SkillId = skillId };
            var posts = await _connection.QueryAsync<Master_skill>(query, parameters);
            return posts.ToList();

        }

        public async Task<int> UpdateAsync(long id, MasterViewModel masters)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"UPDATE public.masters " +
                   $"SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, " +
                       $"phone_number_confirmed=@PhoneNumberConfirmed, image_path=@ImagePath, " +
                            $"is_free=@IsFree, updated_at=@UpdatedAt " +
                                $"WHERE id = @Id";

                var parameters = new Master()
                {
                    Id = id,
                    FirstName = masters.FirstName,
                    LastName = masters.LastName,
                    PhoneNumber = masters.PhoneNumber,
                    PhoneNumberConfirmed = masters.PhoneNumberConfirmed,
                    ImagePath = masters.ImagePath,
                    IsFree = masters.IsFree,
                    UpdatedAt = masters.UpdatedAt,
                };

                var res = await _connection.ExecuteAsync(query, parameters);

                return res;
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

        public async Task<int> UpdateAsync(long id, Master entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"UPDATE public.masters" +
                    $"SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, " +
                        $"phone_number_confirmed=@PhoneNumberConfirmed, image_path=@ImagePath, password_hash=@PasswordHash, " +
                            $"salt=@Salt, is_free=@IsFree, created_at=@CreatedAt, updated_at=@UpdatedAt" +
                                $"WHERE id = {id}";

                var res = await _connection.ExecuteAsync(query, entity);

                return res;
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
